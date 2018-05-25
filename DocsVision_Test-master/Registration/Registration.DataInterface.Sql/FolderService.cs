﻿using System;
using Registration.DatabaseFactory;
using Registration.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataInterface.Sql
{
    abstract public class FolderService : IFolderService
    {
        private DatabaseService _databaseService;
        const string SpGetReceivers = "sp_GetReceivers";

        const string Name = "name";
        const string IdWorker = "idWorker";
        const string IdLetterColumn = "@idLetter";

        public FolderService(DatabaseService _databaseService)
        {
            this._databaseService = _databaseService;
        }

        public DatabaseService DatabaseService
        {
            get { return _databaseService; }
        }

        abstract public int GetCountLettersInFolder(Guid folderId, Guid ownerId);

        abstract public IEnumerable<LetterView> GetLettersInFolder(Guid folderId, Guid ownerId);

        public void GetReceivers(Guid letterId, ref IDictionary<Guid, string> receivers)
        {
            using (IDatabaseConnection connection = DatabaseService.CreateConnection())
            {
             var command = DatabaseService.CreateStoredProcCommand(SpGetReceivers, connection);
                DatabaseService.AddParameterWithValue(IdLetterColumn, letterId, command);

                using (IDatabaseReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        receivers.Add(reader.GetGuid(IdWorker), reader.GetString(Name));
                    }
                    if (receivers.Count == 0)
                    {
                        throw new ArgumentException($"Сообщения с таким id {letterId} нет!");
                    }
                }
            }
        }

    }
}
