using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.DataInterface
{
    public interface IWorkerService
    {
        Worker AuthorizationWorker(string login, string password);
        Worker Create(Worker worker);
        Worker Get( string login);
        IEnumerable<Worker> GetAllWorkers();
        string GetWorkerName(Guid workerIId);
    }
}
