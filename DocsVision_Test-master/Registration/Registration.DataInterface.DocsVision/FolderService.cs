using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.DataInterface.DocsVision
{
    abstract public class FolderService : IFolderService
    {
        abstract public int GetCountLettersInFolder(Guid folderId, Guid ownerId);
        abstract public IEnumerable<LetterView> GetLettersInFolder(Guid folderId, Guid ownerId);
        public void GetReceivers(Guid letterId, ref IDictionary<Guid, string> receivers)
        {

        }
    }
}
