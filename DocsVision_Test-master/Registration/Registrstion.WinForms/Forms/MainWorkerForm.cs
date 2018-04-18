using System;
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
        private List<LetterView> _lettersInfo;
        private List<Guid> _foldersId;
        private Dictionary<string, TreeNode> _existPrivateFoldersInTree;
        private Dictionary<string, Folder> _currentPrivateFoldersInTree;
        private Dictionary<string, FolderType> _currentPrivateFoldersTypeInTree;

        private Dictionary<string, TreeNode> _existSharedFoldersInTree;
        private Dictionary<string, Folder> _currentSharedFoldersInTree;
        private IList<LetterType> _letterTypes;

        private int _selectNodeIndex = 0;
        private int _indexOfSelectedRow = 0;

        public MainWorkerForm(IServiceProvider provider)
        {
            InitializeComponent();

            _lettersInfo = new List<LetterView>();
            _foldersId = new List<Guid>();

            briefContentLetterDGV.MouseClick += new MouseEventHandler(briefContentLetterDGV_MouseClick);
            briefContentLetterDGV.CellDoubleClick += new DataGridViewCellEventHandler(briefContentLetterDGV_CellDoubleClick);

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

        public LetterView FullLetter
        {
            set
            {
                if (value != null)
                {
                    LetterType letterType = ClientRequests.GetLetterType(value.Type);

                    ILetterPropertiesUIPlugin clientUIPlugin = ((PluginService)(ServiceProvider.GetService(typeof(PluginService)))).GetLetterPropetiesPlugin(letterType);
                    clientUIPlugin.ReadOnly = true;

                    clientUIPlugin.LetterView = value;

                    this.Size = new Size(splitContainer2.Size.Width + ((Control)clientUIPlugin).Size.Width, Math.Max(splitContainer2.Size.Height, ((Control)clientUIPlugin).Size.Height));

                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add((Control)clientUIPlugin);
                    Refresh();
                }
            }
        }

        private IEnumerable<LetterView> GetWorkerLettersInFolder(Guid workerId, Guid folderId)
        {
            return ClientRequests.GetWorkerLettersInFolder(workerId, folderId);
        }

        public TreeNode MakeHierarchy(ref IEnumerable<Folder> allFolders, ref Dictionary<string, TreeNode> existFoldersInTree, Folder folder, FolderType folderType, ref StringBuilder path)
        {
            TreeNode n = new TreeNode();
            Folder fParent = new Folder();
            foreach (Folder f in allFolders)
            {
                if (f.Id == folder.ParentId)
                {
                    fParent = f;
                    FolderType newFolderType = ClientRequests.GetFolderType(f.Type);

                    n = MakeHierarchy(ref allFolders, ref existFoldersInTree, f, newFolderType, ref path);
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
                count = ClientRequests.GetCountLetterInFolder(folder.Id);
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }

            if (count > 0)
            {
                newNode.Text += " " + count.ToString();
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
            _currentPrivateFoldersInTree.Add(newNode.FullPath, folder);
            _currentPrivateFoldersTypeInTree.Add(newNode.FullPath, folderType);

            return newNode;
        }

        private void InitializeTreeView()
        {
            foldersTV.Nodes.Clear();

            IEnumerable<Folder> privateFolder = ClientRequests.GetAllWorkerFolders(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            List<Guid> folderUsed = new List<Guid>();
            _existPrivateFoldersInTree = new Dictionary<string, TreeNode>();
            _currentPrivateFoldersInTree = new Dictionary<string, Folder>();
            _currentPrivateFoldersTypeInTree = new Dictionary<string, FolderType>();

            foreach (Folder folder in privateFolder)
            {
                if (!folderUsed.Contains(folder.Id))
                {
                    StringBuilder path = new StringBuilder();
                    FolderType folderType = ClientRequests.GetFolderType(folder.Type);
                    MakeHierarchy(ref privateFolder, ref _existPrivateFoldersInTree, folder, folderType, ref path);
                }
            }

            IEnumerable<Folder> sharedFolder = ClientRequests.GetAllWorkerFolders(Guid.Empty);
            folderUsed = new List<Guid>();
            _existSharedFoldersInTree = new Dictionary<string, TreeNode>();
            _currentSharedFoldersInTree = new Dictionary<string, Folder>();

            foreach (Folder folder in sharedFolder)
            {
                if (!folderUsed.Contains(folder.Id))
                {
                    StringBuilder path = new StringBuilder();
                    FolderType folderType = ClientRequests.GetFolderType(folder.Type);
                    MakeHierarchy(ref sharedFolder, ref _existSharedFoldersInTree, folder, folderType, ref path);
                }
            }
        }

        private void FillBriefContentLetterDGV()
        {
            int select = _indexOfSelectedRow;

            if (briefContentLetterDGV.Rows.Count != 0)
            {
                select = briefContentLetterDGV.CurrentRow.Index;
            }

            if (select > briefContentLetterDGV.Rows.Count)
            {
                select = (select == 0 ? 0 : select - 1);
            }

            briefContentLetterDGV.Rows.Clear();
            _lettersInfo.Clear();

            Folder folder = _currentPrivateFoldersInTree[foldersTV.SelectedNode.FullPath];
            FolderType folderType = _currentPrivateFoldersTypeInTree[foldersTV.SelectedNode.FullPath];

            Guid folderId = folder.Id;

            DataGridViewRow row = briefContentLetterDGV.RowTemplate;

            IEnumerable<LetterView> letters = new List<LetterView>();
            try
            {
                letters = GetWorkerLettersInFolder(folderId, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);
            }
            catch (Exception ex)
            {
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
            }
        }

        private void InitializeMainWorkerForm()
        {
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

        private void briefContentLetterDGV_MouseClick(object sender, MouseEventArgs e)
        {
            deleteLetterToolStripMenuItem.Enabled = true;

            Folder folder = _currentPrivateFoldersInTree[foldersTV.SelectedNode.FullPath];
            Guid folderId = folder.Id;
            if (0 < briefContentLetterDGV.Rows.Count)
            {
                int selectRowIndex = briefContentLetterDGV.CurrentRow.Index;

                briefContentLetterDGV.Rows[selectRowIndex].DefaultCellStyle.BackColor = Color.White;

                LetterIsRead(_lettersInfo[selectRowIndex].Id, ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);

                FullLetter = _lettersInfo[selectRowIndex];
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

            List<System.Windows.Forms.ToolStripMenuItem> items = new List<System.Windows.Forms.ToolStripMenuItem>();

            compose.DropDown.Items.Clear();
            int i = 0;
            foreach (LetterType letterType in _letterTypes)
            {
                items.Add(new System.Windows.Forms.ToolStripMenuItem(letterType.Name));
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

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GetIndexOfSelectedLetter(((ToolStripMenuItem)sender).ToString());
            LetterType letterType = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterType;

            if (index >= 0 && null != _letterTypes[index])
            {
                letterType.Id = _letterTypes[index].Id;
                letterType.Name = _letterTypes[index].Name;
                letterType.TypeClientUI = _letterTypes[index].TypeClientUI;

                using (var makeLetterForm = new Forms.MakeLetterForm(ServiceProvider))
                {
                    makeLetterForm.ShowDialog();
                }
                InitializeMainWorkerForm();
            }

        }

        private void MainWorkerForm_Load(object sender, EventArgs e)
        {
            InitializeClientService();
            InitializeMessageService();

            using (var form = new Registration(ServiceProvider))
            {
                form.ShowDialog();
            }
            if (((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).CloseReason == CloseReason.UserClosing) { Close(); }

            try
            {
                InitializeMainWorkerForm();
                Timer timer = new Timer();
                timer.Interval = (2000); // 2 sec
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            catch (Exception ex)
            {
                NLogger.Logger.Trace(ex.ToString());
            }
        }

        private void Compose_Click(object sender, EventArgs e)
        {
        }

        private void DeleteLetter(LetterView letterView, Guid workerId)
        {
            ClientRequests.DeleteLetter(letterView, workerId);
        }

        private void DeleteLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_lettersInfo.Count == 0 || briefContentLetterDGV.SelectedCells.Count == 0)
            {
                MessageService.ErrorMessage(Message.MessageResource.LetterNotSelect);
            }
            else
            if (MessageService.QuestionMessage(Message.MessageResource.DeleteLetter) == DialogResult.Yes)
            {
                _indexOfSelectedRow = briefContentLetterDGV.CurrentCell.RowIndex;

                DeleteLetter(_lettersInfo[_indexOfSelectedRow], ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).Worker.Id);

                InitializeMainWorkerForm();
            }
        }

        private void briefContentLetterDGV_CellDoubleClick(object sender, EventArgs e)
        {
            int indexOfSelectedRow = briefContentLetterDGV.CurrentCell.RowIndex;

            ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedLetterView = _lettersInfo[indexOfSelectedRow];


            using (var fullContentLetterForm = new FullContentLetterForm(ServiceProvider))
            {
                if (fullContentLetterForm.ShowDialog() == DialogResult.OK) { }
            }
            FillBriefContentLetterDGV();
        }

        private void MakeMenuForFolder(Point locationPoint)
        {
            foldersTV.ContextMenuStrip = contextMenuStrip1;
        }

        private void foldersTV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Folder folder = ((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder;
            folder.Id = _currentPrivateFoldersInTree[e.Node.FullPath].Id;
            folder.Name = _currentPrivateFoldersInTree[e.Node.FullPath].Name;
            folder.OwnerId = _currentPrivateFoldersInTree[e.Node.FullPath].OwnerId;
            folder.ParentId = _currentPrivateFoldersInTree[e.Node.FullPath].ParentId;
            folder.Type = _currentPrivateFoldersInTree[e.Node.FullPath].Type;

            foldersTV.SelectedNode = e.Node;

            bool find = false;
            _selectNodeIndex = 0;
            foreach (var value in _existPrivateFoldersInTree.Values)
            {
                if (value == e.Node)
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
                    if (value == e.Node)
                    {
                        find = true;
                        break;
                    }
                    ++_selectNodeIndex;
                }
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

        private void timer_Tick(object sender, EventArgs e)
        {
            InitializeTreeView();
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
        }


        private void createFolderTSMI_Click(object sender, EventArgs e)
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

        private void changeFolderTSMI_Click(object sender, EventArgs e)
        {
            using (var renameForm = new Forms.RenameFolderForm(ServiceProvider))
            {
                renameForm.ShowDialog();
            }
        }

        private void deleteFolderTSMI_Click(object sender, EventArgs e)
        {
            if (MessageService.QuestionMessage(Message.MessageResource.DeleteFolder) == DialogResult.Yes)
            {
                ClientRequests.DeleteFolder(((ApplicationState)ServiceProvider.GetService(typeof(ApplicationState))).SelectedFolder.Id);
            }
        }

    }
}
