using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;
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
            using (IDatabaseConnection connection = DatabaseService.CreateConnection())
            {
                var command = DatabaseService.CreateStoredProcCommand(SpGetCountLettersInInboxFolder, connection);
                DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);
                DatabaseService.AddParameterWithValue(IdOwnerColumn, ownerId, command);

                using (IDatabaseReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt(CountLetters);
                    }
                    throw new Exception($"Folder {folderId} isn't exist!");
                }
            }
        }

        override public IEnumerable<LetterView> GetLettersInFolder(Guid folderId, Guid ownerId)
        {
            using (IDatabaseConnection connection = DatabaseService.CreateConnection())
            {
               var command = DatabaseService.CreateStoredProcCommand(SpGetLettersFromInboxFolder, connection);
                DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);
                DatabaseService.AddParameterWithValue(IdOwnerColumn, ownerId, command);

                using (IDatabaseReader reader = command.ExecuteReader())
                {
                    var allLettersViewInSentFolder = new List<LetterView>();
                    while (reader.Read())
                    {
                        var letterView = new LetterView()
                        {
                            Id = reader.GetGuid(IdLetter),
                            Name = reader.GetString(NameLetter),
                            IdSender = reader.GetGuid(IdSender),
                            Text = reader.GetString(Text),
                            Date = reader.GetDateTime(Date),
                            SenderName = reader.GetString(NameWorker),
                            IdFolder = folderId,
                            IsRead = reader.GetBool(IsRead),
                            Type = reader.GetInt(TypeLetter),
                            ExtendedData = reader.GetString(ExtendedData)
                        };

                        IDictionary<Guid, string> receivers = new Dictionary<Guid, string>();
                        GetReceivers(letterView.Id, ref receivers);
                        foreach (KeyValuePair<Guid, string> idAndName in receivers)
                        {
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
