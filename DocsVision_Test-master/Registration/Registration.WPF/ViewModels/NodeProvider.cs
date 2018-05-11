using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WPF.ViewModels
{
    public class NodeProvider
    {
        private readonly WPF.Models.DirectoryNode _rootDirectoryNode;

        private IList<Guid> _existFoldersInTree = new List<Guid>();

        private void MakeTree(Folder folder, IEnumerable<Folder> folders, ref Models.DirectoryNode node, Folder selectedFolder)
        {
            foreach (var f in folders)
            {
                if (folder.Id == f.ParentId)
                {
                    Models.DirectoryNode newNode;
                    if (null != selectedFolder && selectedFolder.Id == f.Id)
                        newNode = new WPF.Models.DirectoryNode { Name = f.Name, Folder = f, IsSelected = true };
                    else
                        newNode = new WPF.Models.DirectoryNode { Name = f.Name, Folder = f, IsSelected = false };


                    MakeTree(f, folders, ref newNode, selectedFolder);
                    node.AddDirNode(newNode);
                }
            }

        }

        private void InitialiseMakeTree(IEnumerable<Folder> folders, Folder selectedFolder)
        {
            foreach (var folder in folders)
            {
                if (!_existFoldersInTree.Contains(folder.Id) && folder.ParentId == Guid.Empty)
                {
                    Models.DirectoryNode node;
                    if (null != selectedFolder && selectedFolder.Id == folder.Id)
                        node = new WPF.Models.DirectoryNode { Name = folder.Name, Folder = folder, IsSelected = true };
                    else
                        node = new WPF.Models.DirectoryNode { Name = folder.Name, Folder = folder, IsSelected = false};

                    MakeTree(folder, folders, ref node, selectedFolder);
                    _rootDirectoryNode.AddDirNode(node);
                    _existFoldersInTree.Add(folder.Id);
                }
            }
        }

        public NodeProvider(IEnumerable<Folder> privateFolders, IEnumerable<Folder> sharedFolders, Folder selectedFolder)
        {
            _rootDirectoryNode = new WPF.Models.DirectoryNode { Name = string.Empty, IsSelected = false };
            InitialiseMakeTree(privateFolders, selectedFolder);
            InitialiseMakeTree(sharedFolders, selectedFolder);
            if (null == selectedFolder)
                _rootDirectoryNode.IsSelected = true;
        }

        public List<WPF.Models.Node> DirItems => _rootDirectoryNode.Traverse(_rootDirectoryNode);
    }
}
