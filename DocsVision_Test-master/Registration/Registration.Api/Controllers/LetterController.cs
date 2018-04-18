using Registration.DataInterface;
using Registration.DatabaseFactory;
using Registration.DataInterface.Sql;
using Registration.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;
using System.ComponentModel.Design;
using System.Web;

namespace Registration.Api.Controllers
{
    public class LetterController : ApiController
    {
        private static ServiceContainer _serviceContainer = (ServiceContainer)HttpContext.Current.Application["serviceContainer"];

        private IDictionary<string, ILetterService> _existLetterService = new Dictionary<string, ILetterService>();

        private ILetterService getLetterService(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            ILetterService letterService;
            if (!_existLetterService.TryGetValue(databaseName, out letterService))
            {
                DatabaseService _databaseService = ((IDatabasesService)_serviceContainer.GetService(typeof(IDatabasesService))).GetDatabasesService()[databaseName];
                letterService = new LetterService(_databaseService);
                _existLetterService.Add(databaseName, letterService);
            }
       
            return letterService;
        }

        [HttpPost]
        [Route("api/{databaseName}/letter")]
        public Letter Create([FromBody] LetterView letter, string databaseName)
        {
            return getLetterService(databaseName).Create(letter); 
        }

        [HttpGet]
        [Route("api/{databaseName}/letter/{folderId}/{workerId}")]
        public LetterView Get(Guid folderId, Guid workerId, string databaseName)
        {
            return getLetterService(databaseName).Get(folderId, workerId);
        }

        [HttpDelete]
        [Route("api/{databaseName}/letter/{idLetter}/worker/{idWorker}/folder/{idFolder}")]
        public void Delete(Guid idLetter, Guid idWorker, Guid idFolder, string databaseName)
        {
            getLetterService(databaseName).Delete(idLetter, idWorker, idFolder);
        }

        [HttpPut]
        [Route("api/{databaseName}/letter/{letterId}/{workerId}")]
        public bool LetterIsRead(Guid letterId, Guid workerId, [FromBody] int isRead, string databaseName)
        {
            getLetterService(databaseName).ChangeLetterIsRead(letterId, workerId);
            return true; 
        }

        [HttpGet]
        [Route("api/{databaseName}/letter/types")]
        public IEnumerable<LetterType> GetAllLetterTypes(string databaseName)
        {
            return getLetterService(databaseName).GetAllLetterTypes();
        }

        [HttpGet]
        [Route("api/{databaseName}/letter/{letterTypeId}/type")]
        public LetterType GetLetterType(int letterTypeId, string databaseName)
        {
            return getLetterService(databaseName).GetLetterType(letterTypeId);
        }
    }
}
