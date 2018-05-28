using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface
{
    public interface IWorkerService
    {
        IWorker AuthorizationWorker(string login, string password);
        IWorker Create(IWorker worker);
        IWorker Get(string login);
        IEnumerable<IWorker> GetAllWorkers();
        string GetWorkerName(Guid workerIId);
    }
}
