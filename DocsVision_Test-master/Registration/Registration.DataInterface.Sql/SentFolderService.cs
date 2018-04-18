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
    public class SentFolderService : FolderService
    {
        const string SpGetLettersFromSentFolder = "sp_GetLettersFromSentFolder";
        const string SpGetCountLettersInSentFolder = "sp_GetCountLettersInSentFolder";


        const string IdFolderColumn = "@idFolder";

        const string IdLetter = "idLetter";
        const string NameLetter = "nameLetter";
        const string IdSender = "idSender";
        const string Text = "text";
        const string Date = "date";
        const string NameWorker = "nameWorker";
        const string CountLetters = "countLetters";
        const string TypeLetter = "type";
        const string ExtendedData = "extendedData";

        public SentFolderService(DatabaseService _databaseService) : base(_databaseService) {}

        override public int GetCountLettersInFolder(Guid folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetCountLettersInSentFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);

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

        override public IEnumerable<LetterView> GetLettersInFolder(Guid folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetLettersFromSentFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);

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
                                Type = reader.GetInt32(reader.GetOrdinal(TypeLetter)),
                                ExtendedData = reader.GetString(reader.GetOrdinal(ExtendedData)),
                                IsRead = true
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
}
