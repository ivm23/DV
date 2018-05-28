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
        public Model.Worker AuthorizationWorker(string login, string password, string databaseName)
        {
            return new Model.Worker(getWorkerService(databaseName).AuthorizationWorker(login, password));
        }

        [HttpPost]
        [Route("api/{databaseName}/worker")]
        public Model.Worker Create([FromBody] Model.Worker worker, string databaseName)
        {
            return new Model.Worker(getWorkerService(databaseName).Create((IWorker)worker));
        }

        [HttpGet]
        [Route("api/{databaseName}/worker/{login}")]
        public Model.Worker Get(string login, string databaseName)
        {
            return new Model.Worker(getWorkerService(databaseName).Get(login));
        }

        [HttpGet]
        [Route("api/{databaseName}/workers")]
        public IEnumerable<Model.Worker> GetAllWorkers(string databaseName)
        {
            return (IEnumerable<Model.Worker>)getWorkerService(databaseName).GetAllWorkers();
        }

        [HttpGet]
        [Route("api/{databaseName}/worker/{workerId}/name")]
        public string GetWorkerName(Guid workerId, string databaseName)
        {
            return getWorkerService(databaseName).GetWorkerName(workerId);
        }

    }
}
