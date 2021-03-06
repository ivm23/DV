﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Registration.Model;
using Registration.ClientInterface;
using System.ComponentModel.Design;
using Registration.Logger;
using System.Text;
using System.ComponentModel;

namespace Registration.WinForms.Forms
{
    internal partial class MainWorkerForm : Form
    {
        private IClientRequests _clientRequests;
        private readonly IServiceProvider _serviceProvider;
        private Message.IMessageService _messageService;

        private IList<LetterView> _lettersInfo;

        private Dictionary<string, TreeNode> _existPrivateFoldersInTree;
        private Dictionary<string, Folder> _currentPrivateFoldersInTree;

        private Dictionary<string, TreeNode> _existSharedFoldersInTree;
        private Dictionary<string, Folder> _currentSharedFoldersInTree;

        private IList<LetterType> _letterTypes;

        private IDictionary<int, LetterView> _letters;

        private Guid _index;

        private int _selectNodeIndex = 0;

        private IDictionary<int, int> _selectedRowInSelectedFolder = new Dictionary<int, int>();

        private IDictionary<string, ILetterPropertiesUIPlugin> _letterPropertiesUIPlugins;

        public MainWorkerForm(IServiceProvider provider)
        {
            InitializeComponent();

            _lettersInfo = new List<LetterView>();


            _letters = new Dictionary<int, LetterView>();
            _index = Guid.Empty;

            briefContentLetterDGV.MouseClick += new MouseEventHandler(briefContentLetterDGV_MouseClick);
            briefContentLetterDGV.CellDoubleClick += new DataGridViewCellEventHandler(briefContentLetterDGV_CellDoubleClick);

            briefContentLetterDGV.KeyUp += new KeyEventHandler(briefContentLetterDGV_KeyPress);

            foldersTV.NodeMouseClick += new TreeNodeMouseClickEventHandler(foldersTV_NodeMouseClick);

            _serviceProvider = provider;
        }

        private IServiceProvider ServiceProvider => _serviceProvider;

        private IClientRequests ClientRequests
        {
            get { return _clientRequests; }
        }

        private Message.IMessageService MessageService
        {
            get { return _messageService; }
        }

        private void disableControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = false;
            }
        }

        private void enableControl(Control controlForEnable)
        {
            controlForEnable.Visible = true;
        }

        public void SetFullLetter(LetterView letterView)
        {
                if (letterView != null)
                {
                    LetterType letterType = ClientRequests.GetLetterType(letterView.Type);
                    ILetterPropertiesUIPlugin clientUIPlugin;

                    disableControls(splitContainer1.Panel2.Controls);

                    if (!_letterPropertiesUIPlugins.TryGetValue(letterType.TypeClientUI, out clientUIPlugin))
                    {
                        clientUIPlugin = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetLetterPropetiesPlugin(letterType);
                        _letterPropertiesUIPlugins.Add(letterType.TypeClientUI, clientUIPlugin);

                        splitContainer1.Panel2.Controls.Add((Control)clientUIPlugin);
                    }

                    clientUIPlugin.LetterView = letterView;
                    clientUIPlugin.ReadOnly = true;

                    this.Size = new Size(splitContainer2.Size.Width + ((Control)clientUIPlugin).Size.Width, Math.Max(splitContainer2.Size.Height, ((Control)clientUIPlugin).Size.Height));

                    enableControl((Control)clientUIPlugin);
                }
        }

        private IEnumerable<LetterView> GetWorkerLettersInFolder(Guid workerId, Guid folderId)
        {
            return ClientRequests.GetWorkerLettersInFolder(workerId, folderId);
        }

        public TreeNode MakeHierarchy(ref IEnumerable<Folder> allFolders, ref Dictionary<string, TreeNode> existFoldersInTree, Folder folder, FolderType folderType, ref StringBuilder path, ref Dictionary<string, Folder> currentFoldersInTree)
        {
            TreeNode n = new TreeNode();
            Folder fParent = new Folder();
            foreach (Folder f in allFolders)
            {
                if (f.Id == folder.ParentId)
                {
                    fParent = f;
                    FolderType newFolderType = ClientRequests.GetFolderType(f.Type);

                    n = MakeHierarchy(ref allFolders, ref existFoldersInTree, f, newFolderType, ref path, ref currentFoldersInTree);
                    break;
                }
            }

            path.Append(folder.Name + "\\");
            foreach (var temp in existFoldersInTree)
            {
                if (temp.Key == path.ToString())
                {
                    return temp.Value;
                }
            }

            TreeNode newNode = new TreeNode(folder.Name);
            existFoldersInTree.Add(path.ToString(), newNode);
            int count = 0;
            try
            {
                count = ClientRequests.GetCountLetterInFolder(folder.Id, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }

            if (count > 0)
            {
                newNode.Text += " (" + count.ToString() + ")";
                newNode.NodeFont = new Font(foldersTV.Font, FontStyle.Bold); ;
            }

            if (folder.ParentId == Guid.Empty)
            {
                foldersTV.Nodes.Add(newNode);
            }
            else
            {
                n.Nodes.Add(newNode);
            }
            currentFoldersInTree.Add(newNode.FullPath, folder);

            return newNode;
        }

        private void InitializeMakeHierarchy(ref IEnumerable<Folder> folders, ref Dictionary<string, TreeNode> _existFoldersInTree, ref Dictionary<string, Folder> _currentFoldersInTree)
        {
            List<Guid> folderUsed = new List<Guid>();
            _existFoldersInTree = new Dictionary<string, TreeNode>();
            _currentFoldersInTree = new Dictionary<string, Folder>();

            foreach (Folder folder in folders)
            {
                if (!folderUsed.Contains(folder.Id))
                {
                    StringBuilder path = new StringBuilder();
                    FolderType folderType = ClientRequests.GetFolderType(folder.Type);
                    MakeHierarchy(ref folders, ref _existFoldersInTree, folder, folderType, ref path, ref _currentFoldersInTree);
                }
            }
        }


        private void InitializeTreeView()
        {
            foldersTV.Nodes.Clear();

            IEnumerable<Folder> privateFolder = ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            InitializeMakeHierarchy(ref privateFolder, ref _existPrivateFoldersInTree, ref _currentPrivateFoldersInTree);

            IEnumerable<Folder> sharedFolder = ClientRequests.GetAllWorkerFolders(Guid.Empty);
            InitializeMakeHierarchy(ref sharedFolder, ref _existSharedFoldersInTree, ref _currentSharedFoldersInTree);
            
            string findKey = string.Empty;
            findSelectedNodeKey(ref findKey);
            TreeNode e;
            if (_existPrivateFoldersInTree.ContainsKey(findKey))
            {
                e = _existPrivateFoldersInTree[findKey];
            }
            else
            if (_existSharedFoldersInTree.ContainsKey(findKey))
            {
                e = _existSharedFoldersInTree[findKey];
            }
            else
            {
                e = foldersTV.Nodes[0];
            
            }

            InitializeSelectedFolder(e);

            foldersTV.SelectedNode = e;

            ChangeSelectionNodeIndex(e);
            UpdateSelectedRowDictionary(false);
        }


        private void FillBriefContentLetterDGV()
        {
            int select = _selectedRowInSelectedFolder[_selectNodeIndex];

            if (select > briefContentLetterDGV.Rows.Count)
            {
                select = (select == 0 ? 0 : select - 1);
            }

            briefContentLetterDGV.Rows.Clear();
            _lettersInfo.Clear();
            _letters.Clear();

            Folder folder;

            if (_currentPrivateFoldersInTree.ContainsKey(foldersTV.SelectedNode.FullPath))
                folder = _currentPrivateFoldersInTree[foldersTV.SelectedNode.FullPath];
            else
                if (_currentSharedFoldersInTree.ContainsKey(foldersTV.SelectedNode.FullPath))
                folder = _currentSharedFoldersInTree[foldersTV.SelectedNode.FullPath];
            else
                throw new IndexOutOfRangeException();

            Guid folderId = folder.Id;

            DataGridViewRow row = briefContentLetterDGV.RowTemplate;

            IEnumerable<LetterView> letters = new List<LetterView>();

            try
            {
                letters = GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            }
            catch (Exception ex)
            {
                deleteLetterToolStripMenuItem.Enabled = false;
                NLogger.Logger.Trace(ex.ToString());
            }


            foreach (LetterView letter in letters)
            {
                _lettersInfo.Add(letter);
                row.DefaultCellStyle.BackColor = Color.White;

                if (!letter.IsRead)
                {
                    row.DefaultCellStyle.BackColor = Color.AliceBlue;
                }

                briefContentLetterDGV.Rows.Add(letter.Date.ToString(), letter.Name, letter.SenderName);
            }

            if (select < briefContentLetterDGV.Rows.Count)
            {
                briefContentLetterDGV.Rows[select].Selected = true;
                ShowBriefContentLetter();
            }
            if (0 < _lettersInfo.Count)
                deleteLetterToolStripMenuItem.Enabled = true;
        }


        private void InitializeMainWorkerForm()
        {
            _letterPropertiesUIPlugins = new Dictionary<string, ILetterPropertiesUIPlugin>();
            InitializeTreeView();

            InitializeNewLetterMenu();

            foldersTV.ExpandAll();

            string findKey = string.Empty;
            findSelectedNodeKey(ref findKey);
            if (_existPrivateFoldersInTree.ContainsKey(findKey))
            {
                foldersTV.SelectedNode = _existPrivateFoldersInTree[findKey];
            }
            else
            {
                foldersTV.SelectedNode = _existSharedFoldersInTree[findKey];
            }

            FillBriefContentLetterDGV();
        }

        private void LetterIsRead(Guid letterId, Guid workerId)
        {
            ClientRequests.LetterIsRead(letterId, workerId);
        }

        private void ShowBriefContentLetter()
        {
            deleteLetterToolStripMenuItem.Enabled = true;
            Folder folder;

            if (_currentPrivateFoldersInTree.ContainsKey(foldersTV.SelectedNode.FullPath))
            {
                folder = _currentPrivateFoldersInTree[foldersTV.SelectedNode.FullPath];
            }
            else
                  if (_currentSharedFoldersInTree.ContainsKey(foldersTV.SelectedNode.FullPath))
            {
                folder = _currentSharedFoldersInTree[foldersTV.SelectedNode.FullPath];
            }
            else
                throw new IndexOutOfRangeException();

            Guid folderId = folder.Id;
            if (0 < briefContentLetterDGV.Rows.Count)
            {
                int selectRowIndex = briefContentLetterDGV.CurrentRow.Index;

                briefContentLetterDGV.Rows[selectRowIndex].DefaultCellStyle.BackColor = Color.White;

                LetterIsRead(_lettersInfo[selectRowIndex].Id, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);

                SetFullLetter(_lettersInfo[selectRowIndex]);
            }
        }

        private void briefContentLetterDGV_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ShowBriefContentLetter();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }
        }

        private void InitializeClientService()
        {
            _clientRequests = (IClientRequests)ServiceProvider.GetService(typeof(IClientRequests));
        }

        private void InitializeMessageService()
        {
            _messageService = (Message.IMessageService)ServiceProvider.GetService(typeof(Message.IMessageService));
        }

        private void InitializeNewLetterMenu()
        {
            _letterTypes = (List<LetterType>)(ClientRequests.GetAllLetterTypes());

            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            compose.DropDown.Items.Clear();
            int i = 0;
            foreach (LetterType letterType in _letterTypes)
            {
                items.Add(new ToolStripMenuItem(letterType.Name));
                items[i].Click += new EventHandler(toolStripComboBox1_SelectedIndexChanged);
                ++i;
            }
            compose.DropDownItems.AddRange(items.ToArray());
        }

        private int GetIndexOfSelectedLetter(string letterTypeName)
        {
            int i = 0;
            foreach (LetterType letterType in _letterTypes)
            {
                if (letterType.Name == letterTypeName)
                    return i;
                ++i;
            }
            return i;
        }


        private void InitializeMakeLetterForm(int SelectedTypeLetterIndex)
        {
            LetterType letterType = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType;

            if (SelectedTypeLetterIndex >= 0 && null != _letterTypes[SelectedTypeLetterIndex])
            {
                letterType.Id = _letterTypes[SelectedTypeLetterIndex].Id;
                letterType.Name = _letterTypes[SelectedTypeLetterIndex].Name;
                letterType.TypeClientUI = _letterTypes[SelectedTypeLetterIndex].TypeClientUI;

                using (var makeLetterForm = new Forms.MakeLetterForm(ServiceProvider))
                {
                    if (makeLetterForm.ShowDialog() == DialogResult.OK) { }
                  
                }
                InitializeMainWorkerForm();
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeMakeLetterForm(GetIndexOfSelectedLetter(((ToolStripMenuItem)sender).ToString()));
        }

        private void InitializeTimer()
        {
            Timer timer = new Timer();
            timer.Interval = (2000); // 2 sec
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void MainWorkerForm_Load(object sender, EventArgs e)
        {
            InitializeClientService();
            InitializeMessageService();

            using (var form = new Registration(ServiceProvider))
            {
                form.ShowDialog();
            }
            if (((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CloseReason == CloseReason.UserClosing)
            {
                Close();
            }
            try
            {
                InitializeMainWorkerForm();
                InitializeTimer();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }
        }


        private void DeleteLetter()
        {
            if (_lettersInfo.Count == 0 || briefContentLetterDGV.SelectedCells.Count == 0)
            {
                MessageService.ErrorMessage(Message.MessageResource.LetterNotSelect);
            }
            else

           if (MessageService.QuestionMessage(Message.MessageResource.DeleteLetter) == DialogResult.Yes)
            {
                ClientRequests.DeleteLetter(_lettersInfo[_selectedRowInSelectedFolder[_selectNodeIndex]], ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
                InitializeMainWorkerForm();
            }
        }

        private void saveSelectedRow()
        {
            _selectedRowInSelectedFolder[_selectNodeIndex] = briefContentLetterDGV.CurrentRow.Index;
        }

        private void recoverSelectedRow()
        {
            if (_selectedRowInSelectedFolder[_selectNodeIndex] >= briefContentLetterDGV.Rows.Count)
                --_selectedRowInSelectedFolder[_selectNodeIndex];

            briefContentLetterDGV.Rows[_selectedRowInSelectedFolder[_selectNodeIndex]].Selected = true;
        }

        private void DeleteLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveSelectedRow();
            DeleteLetter();
            recoverSelectedRow();
        }

        private void InitializeFullContentLetterForm()
        {
            int indexOfSelectedRow = briefContentLetterDGV.CurrentCell.RowIndex;
            _selectedRowInSelectedFolder[_selectNodeIndex] = indexOfSelectedRow;
            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView = _lettersInfo[indexOfSelectedRow];


            using (var fullContentLetterForm = new FullContentLetterForm(ServiceProvider))
            {
                if (fullContentLetterForm.ShowDialog() == DialogResult.OK) { }
            }
            FillBriefContentLetterDGV();
        }

        private void briefContentLetterDGV_CellDoubleClick(object sender, EventArgs e)
        {
            saveSelectedRow();
            InitializeFullContentLetterForm();
            recoverSelectedRow();
        }


        private void MakeMenuForFolder(Point locationPoint)
        {
            foldersTV.ContextMenuStrip = contextMenuStrip1;
        }

        private void ChangeSelectionNodeIndex(TreeNode selectedNode)
        {
            bool find = false;
            _selectNodeIndex = 0;
            foreach (var value in _existPrivateFoldersInTree.Values)
            {
                if (value == selectedNode)
                {
                    find = true;
                    break;
                }
                ++_selectNodeIndex;
            }

            if (!find)
            {
                foreach (var value in _existSharedFoldersInTree.Values)
                {
                    if (value == selectedNode)
                    {
                        find = true;
                        break;
                    }
                    ++_selectNodeIndex;
                }
            }
            if (!find)
                throw new ArgumentOutOfRangeException();

        }

        private void InitializeSelectedFolder(TreeNode selectedNode)
        {
            Folder folder = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder;
            Folder currentFolder;
            if (_currentPrivateFoldersInTree.ContainsKey(selectedNode.FullPath))
                currentFolder = _currentPrivateFoldersInTree[selectedNode.FullPath];
            else
                  if (_currentSharedFoldersInTree.ContainsKey(selectedNode.FullPath))
                currentFolder = _currentSharedFoldersInTree[selectedNode.FullPath];
            else
                throw new IndexOutOfRangeException();

            folder.Id = currentFolder.Id;
            folder.Name = currentFolder.Name;
            folder.OwnerId = currentFolder.OwnerId;
            folder.ParentId = currentFolder.ParentId;
            folder.Type = currentFolder.Type;
        }
        
        private void UpdateSelectedRowDictionary(bool useDefaultValue)
        {
            if (!_selectedRowInSelectedFolder.ContainsKey(_selectNodeIndex))
            {
                _selectedRowInSelectedFolder.Add(_selectNodeIndex, 0);
            }
           else
                if (useDefaultValue)
                _selectedRowInSelectedFolder[_selectNodeIndex] = 0;
        }

        private void foldersTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                InitializeSelectedFolder(e.Node);

                foldersTV.SelectedNode = e.Node;

                ChangeSelectionNodeIndex(e.Node);

                UpdateSelectedRowDictionary(true);
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }

            if (e.Button == MouseButtons.Right)
            {
                MakeMenuForFolder(e.Location);
            }
            else
            {
                FillBriefContentLetterDGV();
            }
        }


        private void findSelectedNodeKey(ref string findKey)
        {
            int index = 0;
            bool find = false;
            foreach (string key in _existPrivateFoldersInTree.Keys)
            {
                findKey = key;
                if (index == _selectNodeIndex)
                {
                    find = true;
                    break;
                }
                ++index;
            }
            if (!find)
            {
                foreach (string key in _existSharedFoldersInTree.Keys)
                {
                    findKey = key;
                    if (index == _selectNodeIndex)
                    {
                        find = true;
                        break;
                    }
                    ++index;
                }
            }
        }

        private void ChangeFoldersSelectedNode()
        {
            string findKey = string.Empty;
            findSelectedNodeKey(ref findKey);
            if (_existPrivateFoldersInTree.ContainsKey(findKey))
            {
                foldersTV.SelectedNode = _existPrivateFoldersInTree[findKey];
            }
            else
            if (_existSharedFoldersInTree.ContainsKey(findKey))
            {
                foldersTV.SelectedNode = _existSharedFoldersInTree[findKey];
            }
            else
                throw new ArgumentException();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            InitializeTreeView();
            foldersTV.ExpandAll();
            try
            {
                ChangeFoldersSelectedNode();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }
        }

        private void InitializeCreateFolderForm()
        {
            using (var createFolderForm = new Forms.CreateFolderForm(ServiceProvider))
            {
                IEnumerable<FolderType> folderTypes = ClientRequests.GetAllFolderTypes();

                createFolderForm.FolderType = folderTypes;

                StringBuilder folderPredicat = new StringBuilder();

                if (createFolderForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void createFolderTSMI_Click(object sender, EventArgs e)
        {
            InitializeCreateFolderForm();
        }


        private void changeFolderTSMI_Click(object sender, EventArgs e)
        {
            using (var renameForm = new Forms.RenameFolderForm(ServiceProvider))
            {
                renameForm.ShowDialog();
            }
        }

        private void DeleteFolder()
        {
            ClientRequests.DeleteFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
        }


        private void deleteFolderTSMI_Click(object sender, EventArgs e)
        {
            if (MessageService.QuestionMessage(Message.MessageResource.DeleteFolder) == DialogResult.Yes)
            {
                DeleteFolder();
            }
        }

        private void ArrowUp()
        {
            if (0 <= _selectedRowInSelectedFolder[_selectNodeIndex] - 1)
                --_selectedRowInSelectedFolder[_selectNodeIndex];
        }

        private void ArrowDown()
        {
            if (_selectedRowInSelectedFolder[_selectNodeIndex] + 1 < briefContentLetterDGV.Rows.Count)
                ++_selectedRowInSelectedFolder[_selectNodeIndex];
        }

        private void briefContentLetterDGV_KeyPress(object sender, KeyEventArgs e)
        {
            deleteLetterToolStripMenuItem.Enabled = true;
            if (e.KeyData == Keys.Enter)
            {
                InitializeFullContentLetterForm();
            } else
            if (e.KeyData == Keys.Down)
            {
                ArrowDown();
                ShowBriefContentLetter();
                recoverSelectedRow();
            }
            else
            if (e.KeyData == Keys.Up)
            {
                ArrowUp();
                ShowBriefContentLetter();
                recoverSelectedRow();
            }
            else
                deleteLetterToolStripMenuItem.Enabled = false;
        }

    }
}
