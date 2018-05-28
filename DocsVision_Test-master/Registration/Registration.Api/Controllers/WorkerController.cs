using System;
using System.Collections.Generic;
using System.Web.Http;
using Registration.DataInterface;
using Registration.DataInterface.Sql;
using Registration.DataInterface.DocsVision;
using Registration.Model;
using Registration.DatabaseFactory;
using System.ComponentModel.Design;
using System.Web;


namespace Registration.Api.Controllers
{
    public class WorkerController : ApiController
    {
        private static ServiceContainer _serviceWorkerContainer = (ServiceContainer)HttpContext.Current.Application["serviceContainer"];

        private IDictionary<string, IWorkerService> _existWorkerService = new Dictionary<string, IWorkerService>();

        private IWorkerService getWorkerService(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }
            IWorkerService workerService;
            if (!_existWorkerService.TryGetValue(databaseName, out workerService))
            {
               var a = ((IDatabasesService)_serviceWorkerContainer.GetService(typeof(IDatabasesService))).GetDatabasesService();
                DatabaseService _databaseService = ((IDatabasesService)_serviceWorkerContainer.GetService(typeof(IDatabasesService))).GetDatabasesService()[databaseName];
                workerService = new Registration.DataInterface.DocsVision.WorkerService(_databaseService, databaseName);
                _existWorkerService.Add(databaseName, workerService);
            }
            return workerService;
        }


        [HttpGet]
        [Route("api/{databaseName}/worker/{login}/{password}")]
        public Worker AuthorizationWorker(string login, string password, string databaseName)
        {
            return getWorkerService(databaseName).AuthorizationWorker(login, password);
        }

        [HttpPost]
        [Route("api/{databaseName}/worker")]
        public Worker Create([FromBody] Worker worker, string databaseName)
        {
            return getWorkerService(databaseName).Create(worker);
        }

        [HttpGet]
        [Route("api/{databaseName}/worker/{login}")]
        public Worker Get(string login, string databaseName)
        {
            return getWorkerService(databaseName).Get(login);
        }

        [HttpGet]
        [Route("api/{databaseName}/workers")]
        public IEnumerable<Worker> GetAllWorkers(string databaseName)
        {
            return getWorkerService(databaseName).GetAllWorkers();
        }

        [HttpGet]
        [Route("api/{databaseName}/worker/{workerId}/name")]
        public string GetWorkerName(Guid workerId, string databaseName)
        {
            return getWorkerService(databaseName).GetWorkerName(workerId);
        }

    }
}
