using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
using System.Data;
using Registration.DatabaseFactory;
using System.ComponentModel.Design;
using Registration.DataInterface.Sql;

namespace Registration.DataInterface.Sql
{
    public class InboxFolderService : FolderService
    {
        const string SpGetLettersFromInboxFolder = "sp_GetLettersFromInboxFolder";
        const string SpGetCountLettersInInboxFolder = "sp_GetCountLettersInInboxFolder";

        const string IdFolderColumn = "@idFolder";
        const string IdOwnerColumn = "@idOwner";

        const string IdLetter = "idLetter";
        const string NameLetter = "nameLetter";
        const string IdSender = "idSender";
        const string Text = "text";
        const string Date = "date";
        const string NameWorker = "nameWorker";
        const string IsRead = "isRead";
        const string CountLetters = "countLetters";
        const string TypeLetter = "type";
        const string ExtendedData = "extendedData";

        public InboxFolderService(DatabaseService _databaseService) : base(_databaseService) { }

        override public int GetCountLettersInFolder(Guid folderId, Guid ownerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetCountLettersInInboxFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);
                    DatabaseService.AddParameterWithValue(IdOwnerColumn, ownerId, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(reader.GetOrdinal(CountLetters));
                        }
                        throw new Exception($"Folder {folderId} isn't exist!");
                    }
                }
            }
        }

        override public IEnumerable<LetterView> GetLettersInFolder(Guid folderId, Guid ownerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetLettersFromInboxFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);
                    DatabaseService.AddParameterWithValue(IdOwnerColumn, ownerId, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        var allLettersViewInSentFolder = new List<LetterView>();
                        while (reader.Read())
                        {
                            var letterView = new LetterView()
                            {
                                Id = reader.GetGuid(reader.GetOrdinal(IdLetter)),
                                Name = reader.GetString(reader.GetOrdinal(NameLetter)),
                                IdSender = reader.GetGuid(reader.GetOrdinal(IdSender)),
                                Text = reader.GetString(reader.GetOrdinal(Text)),
                                Date = reader.GetDateTime(reader.GetOrdinal(Date)),
                                SenderName = reader.GetString(reader.GetOrdinal(NameWorker)),
                                IdFolder = folderId,
                                IsRead = reader.GetBoolean(reader.GetOrdinal(IsRead)),
                                Type = reader.GetInt32(reader.GetOrdinal(TypeLetter)),
                                ExtendedData =  reader.GetString(reader.GetOrdinal(ExtendedData))
                            };

                            IDictionary<Guid, string> receivers = new Dictionary<Guid, string>();
                            GetReceivers(letterView.Id, ref receivers);
                            foreach(KeyValuePair<Guid, string> idAndName in receivers) {
                                letterView.IdReceivers.Add(idAndName.Key);
                                letterView.ReceiversName.Add(idAndName.Value);
                            }
                            allLettersViewInSentFolder.Add(letterView);
                        }
                        if (allLettersViewInSentFolder.Count() == 0)
                        {
                            throw new Exception($"Folder {folderId} is empty");
                        }
                        return allLettersViewInSentFolder;
                    }
                }
            }
        }
    }
}
