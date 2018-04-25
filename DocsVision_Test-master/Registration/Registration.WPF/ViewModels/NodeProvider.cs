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

        private void MakeTree(Folder folder, IEnumerable<Folder> folders, ref Models.DirectoryNode node)
        {
            foreach(var f in folders)
            {
                if (folder.Id == f.ParentId )
                {
                    Models.DirectoryNode newNode = new WPF.Models.DirectoryNode { Name = f.Name };
                    MakeTree(f, folders, ref node);
                    node.AddDirNode(newNode);
                }
            }

        }

        private void InitialiseMakeTree(IEnumerable<Folder> folders)
        {
            foreach (var folder in folders)
            {
                if (!_existFoldersInTree.Contains(folder.Id) && folder.ParentId == Guid.Empty)
                {
                    Models.DirectoryNode node = new WPF.Models.DirectoryNode { Name = folder.Name };
                    MakeTree(folder, folders, ref node);
                    _rootDirectoryNode.AddDirNode(node);
                    _existFoldersInTree.Add(folder.Id);
                }
            }
        }

        public NodeProvider(IEnumerable<Folder> privateFolders, IEnumerable<Folder> sharedFolders)
        {
            _rootDirectoryNode = new WPF.Models.DirectoryNode { Name = string.Empty };
            InitialiseMakeTree(privateFolders);
            InitialiseMakeTree(sharedFolders);
            _rootDirectoryNode.IsSelected = true;
        }

        public List<WPF.Models.Node> DirItems => _rootDirectoryNode.Traverse(_rootDirectoryNode);
    }
}
