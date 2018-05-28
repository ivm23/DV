using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectManager.Metadata;
using DocsVision.Platform.ObjectManager.SearchModel;
using Registration.DatabaseFactory;

namespace Registration.DataInterface.DocsVision
{
    public class WorkerService : IWorkerService
    {
        private DatabaseService _databaseService;
        private readonly WorkersDictionary _workersDictionary;

        public WorkerService(DatabaseService _databaseService, string databaseName)
        {
            this._databaseService = _databaseService;
            SessionManager sesionManager = SessionManager.CreateInstance();
            sesionManager.Connect(DatabaseService.connectionString, databaseName);
            _userSession = sesionManager.CreateSession();
            _workersDictionary = new WorkersDictionary(_userSession.CardManager.GetDictionaryData(WorkersDictionaryId););
        }

        public DatabaseService DatabaseService
        {
            get { return _databaseService; }
        }

        private readonly UserSession _userSession;
        private readonly Guid WorkersDictionaryId = new Guid("EBAE354B-9C29-49DC-BDF6-64A81DE27309");
        private readonly Guid WorkersSectionId = new Guid("4274DBA2-D96E-4857-B54C-F6D4C5F7C5E3");
        private const string WorkerLogin = "Login";

        private UserSession UserSession
        {
            get { return _userSession; }
        }

        private WorkersDictionary WorkersDictionary
        {
            get { return _workersDictionary; }
        }
        public IWorker AuthorizationWorker(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException();

           IWorker worker = Get(login);

            if (worker.Password.Equals(password))
                return worker;
                        
            throw new Exception("Worker isn't exist!");
        }

        public IWorker Create(IWorker worker)
        {
            if (null == worker)
                throw new ArgumentNullException();

            RowData newRow = WorkersDictionary.AddNew();

            IWorker newWorker = new Worker(newRow);

            newWorker.Name = worker.Name;
            newWorker.Login = worker.Login;
            newWorker.Password = worker.Password;

            return newWorker;
        }

        public IWorker Get(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException();

            CardData cardDictionary = _userSession.CardManager.GetDictionaryData(WorkersDictionaryId);
            SectionData section = cardDictionary.Sections[WorkersSectionId];

            SectionQuery sectionQuery = UserSession.CreateSectionQuery();
            sectionQuery.SectionId = WorkersSectionId;

            sectionQuery.ConditionGroup.Conditions.AddNew(WorkerLogin, FieldType.String, ConditionOperation.Equals, login);
            var findRow = section.FindRows(sectionQuery.GetXml()).First();

            if (findRow != null)
            {
                return new Worker(findRow);
            }
             
            throw new Exception($"worker with {login} isn't exist!");
        }

        public IEnumerable<IWorker> GetAllWorkers()
        {
            IList<Worker> allWorkers = new List<Worker>();
            CardData cardDictionary = _userSession.CardManager.GetDictionaryData(WorkersDictionaryId);
            SectionData section = cardDictionary.Sections[WorkersSectionId];
            RowDataCollection allRows = section.GetAllRows();

            foreach (var row in allRows)
            {
                allWorkers.Add(new Worker(row));
            }
            return allWorkers;
        }

        public string GetWorkerName(Guid workerId)
        {
            CardData cardDictionary = UserSession.CardManager.GetDictionaryData(WorkersDictionaryId, false);
            SectionData section = cardDictionary.Sections[WorkersSectionId];

            SectionQuery sectionQuery = UserSession.CreateSectionQuery();
            sectionQuery.SectionId = WorkersSectionId;

            sectionQuery.ConditionGroup.Conditions.AddNew(WorkerLogin, FieldType.String, ConditionOperation.Equals, login);
            var findRow = section.FindRows(sectionQuery.GetXml()).First();

            if (findRow != null)
            {
                return new Worker(findRow).ToString();
            }
            throw new Exception($"Worker with id {workerId} isn't exist!");
        }
    }

}
