using Registration.DataInterface;
using Registration.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.ComponentModel.Design;
using System.Web;
using Registration.DatabaseFactory;
using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Configuration;

namespace Registration.Api.Controllers
{
    public class FolderController : ApiController
    {
        private static ServiceContainer _serviceFolderContainer = (ServiceContainer)HttpContext.Current.Application["serviceContainer"];

        private IDictionary<string, IFolderTreeService> _existFolderTreeService = new Dictionary<string, IFolderTreeService>();

        private IFolderTreeService getFolderTreeService(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }
           
            IFolderTreeService folderTreeService;
            if (!_existFolderTreeService.TryGetValue(databaseName, out folderTreeService))
            {
                DatabaseService _databaseService = ((IDatabasesService)_serviceFolderContainer.GetService(typeof(IDatabasesService))).GetDatabasesService()[databaseName];
                folderTreeService = new FolderTreeService(_databaseService);
                _existFolderTreeService.Add(databaseName, folderTreeService);
            }

            return folderTreeService;
        }


        [HttpPost]
        [Route("api/{databaseName}/folder")]
        public Folder CreateFolder([FromBody] Folder folder, string databaseName)
        {           
            return getFolderTreeService(databaseName).Create(folder);
        }

        [HttpGet]
        [Route("api/{databaseName}/allFolders")]
        public string GetHierarchy(string databaseName)
        {
            return getFolderTreeService(databaseName).GetAllFolders();
        }

        [HttpGet]
        [Route("api/{databaseName}/letters/folder/{folderId}/{ownerId}")]
        public IEnumerable<LetterView> GetLettersInFolder(Guid folderId, Guid ownerId, string databaseName)
        {
            return getFolderTreeService(databaseName).GetFolderService(folderId).GetLettersInFolder(folderId, ownerId);
        }

        [HttpGet]
        [Route("api/{databaseName}/count/letters/{folderId}/{ownerId}")]
        public int GetCountLettersInFolder(Guid folderId, Guid ownerId, string databaseName)
        {
            return getFolderTreeService(databaseName).GetFolderService(folderId).GetCountLettersInFolder(folderId, ownerId);
        }

        [HttpPut]
        [Route("api/{databaseName}/folder/update")]
        public Folder ChangeLetterText([FromBody] Folder folder, string databaseName)
        {
            return getFolderTreeService(databaseName).Update(folder);
        }


        [HttpDelete]
        [Route("api/{databaseName}/folder/{folderId}")]
        public void Delete(Guid folderId, string databaseName)
        {
            getFolderTreeService(databaseName).Delete(folderId);
        }

        [HttpGet]
        [Route("api/{databaseName}/folders/{workerId}")]
        public IEnumerable<Folder> GetHierarchy(Guid workerId, string databaseName)
        {
            return getFolderTreeService(databaseName).GetAllWorkerFolders(workerId);
        }

        [HttpGet]
        [Route("api/{databaseName}/folder/types")]
        public IEnumerable<FolderType> GetAllFolderTypes(string databaseName)
        {
            return getFolderTreeService(databaseName).GetAllFolderTypes();
        }

        [HttpGet]
        [Route("api/{databaseName}/folder/{folderTypeId}/type")]
        public FolderType GetFolderType(int folderTypeId, string databaseName)
        {
            return getFolderTreeService(databaseName).GetFolderType(folderTypeId);
        }
    }
}