using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface
{
    public interface IFolderService
    {
        int GetCountLettersInFolder(Guid folderId, Guid ownerId);
        IEnumerable<ILetterView> GetLettersInFolder(Guid folderId, Guid ownerId);
    }
}
