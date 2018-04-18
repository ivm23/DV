using System;
using System.Collections.Generic;
using Registration.Model;
using System.Text;

namespace Registration.ClientInterface
{
    public class ClientRequests : IClientRequests
    {
        private IHttpRequests _httpRequests;
        private string _selectedDatabase;
        const string DatabaseNameMark = "Initial Catalog = ";
        const char SplitMark = ';';
        private string _connectionString;

        public string DatabaseName
        {
            set { _connectionString = value; }
            get { return _connectionString; }
        }

        public ClientRequests()
        {
            _httpRequests = new HttpRequests("http://localhost:57893/api/");
        }

        public string SelectedDatabase
        {
            set
            {
                _selectedDatabase = value;
            }
            get
            {
                return _selectedDatabase;
            }
        }

        private IHttpRequests HttpRequests
        {
            get { return _httpRequests; }
        }

        public bool WorkerIsExist(string workerLogin )
        {
            return (!HttpRequests.GetWorker(workerLogin, _connectionString).isEmpty());
        }

        public Guid AcceptAuthorisation(string workerLogin, string workerPassword)
        {
            Worker worker = HttpRequests.AuthorizationWorker(workerLogin, workerPassword, _connectionString);
            return (worker == null ? Guid.Empty : worker.Id);
        }

        public Guid CreateWorker(string workerName, string workerLogin, string workerPassword)
        {
            Worker newWorker = new Worker()
            {
                Name = workerName,
                Login = workerLogin,
                Password = workerPassword
            };
            return HttpRequests.CreateWorker(newWorker, _connectionString).Id;
        }

        public IEnumerable<LetterView> GetLetterView(Guid id)
        {
            return HttpRequests.GetLettersView(id, _connectionString);
        }

        public bool LetterIsRead(Guid letterId, Guid workerId)
        {
            HttpRequests.LetterIsRead(letterId, workerId, 1, _connectionString);
            return true;
        }

        public IEnumerable<string> GetAllWorkers()
        {
            List<string> nameAndLoginAllWorkers = new List<string>();
            IEnumerable<Worker> allWorkers = HttpRequests.GetAllWorkers(_connectionString);
            foreach (Worker worker in allWorkers)
            {
                nameAndLoginAllWorkers.Add(worker.ToString());
            }
            return nameAndLoginAllWorkers;
        }

        public void CreateLetter(string letterName, Guid senderId, IEnumerable<string> workerNameAndLogin, string letterText, string extendedData, int type)
        {
            Letter newLetter = new Letter()
            {
                Name = letterName,
                Text = letterText,
                IdSender = senderId,
                Type = type,
                ExtendedData = extendedData
            };

            foreach (string worker in workerNameAndLogin)
            {
                string workerLogin = worker.Split('(')[1].Split(')')[0];
                newLetter.IdReceivers.Add(HttpRequests.GetWorker(workerLogin, _connectionString).Id);
            }
            HttpRequests.CreateLetter(newLetter, _connectionString);
        }

        public void DeleteLetter(LetterView letter, Guid idWorker)
        {
            HttpRequests.DeleteLetter(letter.Id, idWorker, letter.IdFolder, _connectionString);
        }

        public IEnumerable<string> GetDatabasesNames()
        {
            return HttpRequests.GetDatabasesNames();
        }

   /*     public IDictionary<string,string> GetDatabaseNamesAndConnectionStrings()
        {
            IEnumerable<string> connectionStrings = GetConnectionStrings();

            IDictionary<string, string> databaseNamesAndConnectionStrings = new Dictionary<string, string>();

            foreach(string connectString in connectionStrings)
            {
                int indexBeginName = connectString.IndexOf(DatabaseNameMark) + DatabaseNameMark.Length;
                int indexEndName = indexBeginName;

                while (connectString[indexEndName] != SplitMark) 
                {
                    ++indexEndName;
                }
                databaseNamesAndConnectionStrings.Add(connectString.Substring(indexBeginName, indexEndName - indexBeginName), _connectionString);
            }
            return databaseNamesAndConnectionStrings;
        }*/

        public string GetWorkerName(Guid workerId)
        {
            return HttpRequests.GetWorkerName(workerId, _connectionString);
        }

        public IEnumerable<Folder> GetAllWorkerFolders(Guid workerId)
        {
            return HttpRequests.GetAllWorkerFolders(workerId, _connectionString);
        }
        public IEnumerable<LetterView> GetWorkerLettersInFolder(Guid folderId, Guid ownerId)
        {
            return HttpRequests.GetWorkerLettersInFolder(folderId, ownerId, _connectionString);
        }
        public Folder GetFolder(Guid folderId)
        {
            return HttpRequests.GetFolder(folderId, _connectionString);
        }

        public Folder CreateFolder(Guid parentId, string name, Guid creatorId, int type, string data)
        {
            return HttpRequests.CreateFolder(new Folder() { ParentId = parentId, Name = name, Type = type, OwnerId = creatorId, Data = data }, _connectionString);
        }

        public Folder UpdateFolder(Guid folderId, string newName)
        {
            Folder folder = new Folder()
            {
                Id = folderId,
                Name = newName
            };
            return HttpRequests.UpdateFolder(folder, _connectionString);
        }

        public void DeleteFolder(Guid folderId)
        {
            HttpRequests.DeleteFolder(folderId, _connectionString);
        }
        public int GetCountLetterInFolder(Guid folderId)
        {
            return HttpRequests.GetCountLettersInFolder(folderId, _connectionString);
        }

        public IEnumerable<FolderType> GetAllFolderTypes()
        {
            return HttpRequests.GetAllFolderTypes(_connectionString);
        }

        public FolderType GetFolderType(int folderTypeId)
        {
            return HttpRequests.GetFolderType(folderTypeId, _connectionString);
        }
        public LetterType GetLetterType(int letterTypeId)
        {
            return HttpRequests.GetLetterType(letterTypeId, _connectionString);
        }

        public IEnumerable<LetterType> GetAllLetterTypes()
        {
            return HttpRequests.GetAllLetterTypes(_connectionString);
        }
    }
}
