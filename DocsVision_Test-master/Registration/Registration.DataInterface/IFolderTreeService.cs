using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface
{
    public interface IFolderTreeService
    {
        Guid GetOwnerId(IFolder folder);
        IFolder Create(IFolder folder);
        void Delete(Guid folderId);
        IFolder Update(IFolder folder);
        string GetAllFolders();
        IEnumerable<IFolder> GetAllWorkerFolders(Guid workerId);
        IEnumerable<IFolderType> GetAllFolderTypes();
        IFolderType GetFolderType(int folderTypeId);

        IFolderService GetFolderService(Guid folderId);
    }
}
