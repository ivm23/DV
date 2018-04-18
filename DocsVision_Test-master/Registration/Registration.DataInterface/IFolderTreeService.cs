using System;
using Registration.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface
{
    public interface IFolderTreeService
    {
        Guid GetOwnerId(Folder folder);
        Folder Create(Folder folder);
        void Delete(Guid folderId);
        Folder Update(Folder folder);
        string GetAllFolders();
        IEnumerable<Folder> GetAllWorkerFolders(Guid workerId);
        IEnumerable<FolderType> GetAllFolderTypes();
        FolderType GetFolderType(int folderTypeId);

        IFolderService GetFolderService(Guid folderId);
    }
}
