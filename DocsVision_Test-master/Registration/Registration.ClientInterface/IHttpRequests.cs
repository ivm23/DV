using System;
using System.Collections.Generic;
using Registration.Model;

namespace Registration.ClientInterface
{
    public interface IHttpRequests
    {
        Letter CreateLetter( Letter letter, string connectionString);
        bool LetterIsRead(Guid idLetter, Guid idWorker, int isRead, string connectionString);
        IEnumerable<LetterView> GetLettersView(Guid idWorker, string connectionString);
        IEnumerable<LetterView> GetWorkerLettersInFolder(Guid workerId, Guid folderId, string connectionString);
        void DeleteLetterFromDatabase(Guid letterId, string connectionString);
        void DeleteLetter(Guid idLetter, Guid idWorker, Guid idFolder, string connectionString);

        Worker CreateWorker(Worker worker, string connectionString);
        Worker GetWorker(string login, string connectionString);
        IEnumerable<Worker> GetAllWorkers(string connectionString);
        Worker AuthorizationWorker(string login, string password, string connectionString);
        string GetWorkerName(Guid workerId, string connectionString);

        IEnumerable<string> GetDatabasesNames();

        IEnumerable<Folder> GetAllWorkerFolders(Guid workerId, string connectionString);

        Folder GetFolder(Guid folderId, string connectionString);
        Folder CreateFolder(Folder folder, string connectionString);
        Folder UpdateFolder(Folder folder, string connectionString);

        int GetCountLettersInFolder(Guid folderId, string connectionString);

        void DeleteFolder(Guid folderId, string connectionString);

        IEnumerable<FolderType> GetAllFolderTypes(string connectionString);

        FolderType GetFolderType(int folderTypeId, string connectionString);

        IEnumerable<LetterType> GetAllLetterTypes(string connectionString);

        LetterType GetLetterType(int letterTypeId, string connectionString);
    }
}
