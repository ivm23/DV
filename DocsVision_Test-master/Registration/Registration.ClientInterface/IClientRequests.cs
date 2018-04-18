using System;
using System.Collections.Generic;
using Registration.Model;

namespace Registration.ClientInterface
{
    public interface IClientRequests
    {
        string DatabaseName { get; set; }
        string SelectedDatabase { set; get; }
        bool WorkerIsExist(string workerLogin);
        Guid AcceptAuthorisation(string workerLogin, string workerPassword);
        Guid CreateWorker(string workerName, string workerLogin, string workerPassword);
        IEnumerable<LetterView> GetLetterView(Guid id);
        bool LetterIsRead(Guid letterId, Guid workerId);
        IEnumerable<string> GetAllWorkers();

        void CreateLetter(string letterName, Guid senderId, IEnumerable<string> workerNameAndLogin, string letterText, string extendedData, int type);
        void DeleteLetter(LetterView letter, Guid idWorker);
      
        string GetWorkerName(Guid workerId);

        IEnumerable<Folder> GetAllWorkerFolders(Guid workerId);
        IEnumerable<LetterView> GetWorkerLettersInFolder(Guid workerId, Guid folderId);
        Folder GetFolder(Guid folderId);
        Folder CreateFolder(Guid parentId, string name, Guid creatorId, int type, string data );
        Folder UpdateFolder(Guid folderId, string newName);

        int GetCountLetterInFolder(Guid folderId);
        void DeleteFolder(Guid folderId);

        IEnumerable<FolderType> GetAllFolderTypes();

        FolderType GetFolderType(int folderTypeId);
        LetterType GetLetterType(int letterTypeId);

        IEnumerable<string> GetDatabasesNames();

        IEnumerable<LetterType> GetAllLetterTypes();
    }
}
