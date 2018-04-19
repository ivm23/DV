using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Registration.Model;
using System.Data;
using System.Text;
using Registration.DatabaseFactory;

namespace Registration.DataInterface.Sql
{
    public class LetterService : ILetterService
    {
        const string SpCreateLetter = "sp_CreateLetter";
        const string SpGetLetter = "sp_GetLetter";
        const string SpGetReceivers = "sp_GetReceivers";
        const string SpDeleteLetter = "sp_DeleteLetter";
        const string SpUpdateLetterIsRead = "sp_UpdateIsRead";
        const string SpGetAllLetterTypes = "sp_GetAllLetterTypes";
        const string SpGetLetterType = "sp_GetLetterType";

        const string IdLetterColumn = "@idLetter";
        const string IdWorkerColumn = "@idWorker";
        const string NameColumn = "@name";
        const string IdSenderColumn = "@idSender";
        const string TextColumn = "@text";
        const string DateColumn = "@date";
        const string IdReceiversColumn = "@idReceivers";
        const string IdFolderColumn = "@idFolder";
        const string IsReadColumn = "@isRead";

        const string IdColumn = "@id";
        const string IsDeleteColumn = "isDelete";
        const string ExtendedDataColumn = "@extendedData";
        const string LetterTypeIdColumn = "@type";

        const string Name = "name";
        const string IdSender = "idSender";
        const string Date = "date";
        const string Text = "text";
        const string SenderName = "senderName";
        const string IsRead = "isRead";
        const string IdWorker = "idWorker";
        const string IdLetter = "idLetter";

        const string ExtendedData = "extendedData";
        const string LetterTypeId = "type";
        const string Id = "id";
        const string TypeClientUI = "typeClientUI";
        

        private DatabaseService _databaseService;
     
        public LetterService(DatabaseService _databaseService)
        {
            this._databaseService = _databaseService;
        }

        public DatabaseService DatabaseService
        {
            get { return _databaseService; }
        }

        public Letter Create(LetterView letter)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                letter.Id = Guid.NewGuid();
                letter.Date = DateTime.Now;
                letter.IsRead = true;

                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpCreateLetter, connection))
                {
                    DatabaseService.AddParameterWithValue(IdLetterColumn, letter.Id, command);
                    DatabaseService.AddParameterWithValue(NameColumn, letter.Name, command);
                    DatabaseService.AddParameterWithValue(IdSenderColumn, letter.IdSender, command);
                    DatabaseService.AddParameterWithValue(TextColumn, letter.Text, command);
                    DatabaseService.AddParameterWithValue(DateColumn, letter.Date, command);
          

                    DataTable data = new DataTable();
                    data.Columns.Add(IdLetter, typeof(Guid));
                    data.Columns.Add(IdWorker, typeof(Guid));
                    data.Columns.Add(IsRead, typeof(bool));

                    foreach (Guid receiverId in letter.IdReceivers)
                    {
                        data.Rows.Add(letter.Id, receiverId, false);
                    }

                    DatabaseService.AddParameterWithValue(IdReceiversColumn, data, command);

                    DatabaseService.AddParameterWithValue(ExtendedDataColumn, letter.ExtendedData, command);
                    DatabaseService.AddParameterWithValue(LetterTypeIdColumn, letter.Type, command);
                    DatabaseService.AddParameterWithValue(IsDeleteColumn, false, command);

                    command.ExecuteNonQuery();
                    return letter;
                }
            }
        }

        public LetterView Get(Guid letterId, Guid workerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetLetter, connection))
                {
                    DatabaseService.AddParameterWithValue(IdLetterColumn, letterId, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new ArgumentException($"Сообщения с таким id {letterId} нет!");
                        }
                        LetterView letter = new LetterView
                        {
                            Id = letterId,
                            Name = reader.GetString(reader.GetOrdinal(Name)),
                            IdSender = reader.GetGuid(reader.GetOrdinal(IdSender)),
                            Text = reader.GetString(reader.GetOrdinal(Text)),
                            Date = reader.GetDateTime(reader.GetOrdinal(Date)),
                            ExtendedData = reader.GetString(reader.GetOrdinal(ExtendedData)),
                            Type = reader.GetInt32(reader.GetOrdinal(LetterTypeId))
                        };
                        List<Guid> receiversId = new List<Guid>();
                        List<string> receiversName = new List<string>();
                        GetReceivers(letterId, ref receiversId, ref receiversName);

                        foreach (Guid rec in receiversId)
                        {
                            letter.IdReceivers.Add(rec);
                        }

                        foreach (string name in receiversName)
                        {
                            letter.ReceiversName.Add(name);
                        }
                        return letter;
                    }
                }
            }
        }

        public void GetReceivers(Guid letterId, ref List<Guid> idReceivers, ref List<String> nameReceivers)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetReceivers, connection))
                {
                    DatabaseService.AddParameterWithValue(IdLetterColumn, letterId, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idReceivers.Add(reader.GetGuid(reader.GetOrdinal(IdWorker)));
                            nameReceivers.Add(reader.GetString(reader.GetOrdinal(Name)));
                        }
                        if (idReceivers.Count == 0)
                        {
                            throw new ArgumentException($"Сообщения с таким id {letterId} нет!");
                        }
                    }
                }
            }
        }

        public void Delete(Guid letterId, Guid workerId, Guid folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpDeleteLetter, connection))
                {
                    DatabaseService.AddParameterWithValue(IdLetterColumn, letterId, command);
                    DatabaseService.AddParameterWithValue(IdWorkerColumn, workerId, command);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeLetterIsRead(Guid letterId, Guid workerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpUpdateLetterIsRead, connection))
                {
                    Letter letter = Get(letterId, workerId);
                    if (!letter.IsRead)
                    {
                        DatabaseService.AddParameterWithValue(IdLetterColumn, letterId, command);
                        DatabaseService.AddParameterWithValue(IdWorkerColumn, workerId, command);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public IEnumerable<LetterType> GetAllLetterTypes()
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetAllLetterTypes, connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        var allTypes = new List<LetterType>();
                        while (reader.Read())
                        {
                            var type = new LetterType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal(Id)),
                                TypeClientUI = reader.GetString(reader.GetOrdinal(TypeClientUI)),
                                Name = reader.GetString(reader.GetOrdinal(Name))
                            };
                            if (!string.IsNullOrEmpty(type.TypeClientUI))
                            {
                                allTypes.Add(type);
                            }
                        }
                        if (allTypes.Count == 0)
                        {
                            throw new Exception("No types");
                        }
                        return allTypes;
                    }
                }
            }
        }
        public LetterType GetLetterType(int letterId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetLetterType, connection))
                {
                    DatabaseService.AddParameterWithValue(IdColumn, letterId, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LetterType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                TypeClientUI = reader.GetString(reader.GetOrdinal(TypeClientUI))
                            };
                        }
                        else
                        {
                            throw new Exception("Such type isn't exist!");
                        }
                    }
                }
            }
        }

    }

}