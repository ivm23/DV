using System;
using System.Collections.Generic;
using Registration.Model;
using System.ComponentModel.Design;
using System.Data;
using System.Text;
using Registration.DatabaseFactory;

namespace Registration.DataInterface
{
    public class FolderTreeService : IFolderTreeService
    {
        const string SpCreateFolder = "sp_CreateFolder";
        const string SpGetFolderOwner = "sp_GetFolderOwner";
        const string SpDeleteFolder = "sp_DeleteFolder";
        const string SpUpdateFolder = "sp_UpdateFolder";
        const string SpGetAllFolders = "sp_GetAllFolders";
        const string SpGetFolders = "sp_GetFolders";
        const string SpGetAllFoldersType = "sp_GetAllFoldersType";
        const string SpGetFolderType = "sp_GetFolderType";
        const string SpGetFolderTypeId = "sp_GetFolderTypeId";


        const string IdFolderColumn = "@idFolder";
        const string IdColumn = "@id";
        const string IdLetterColumn = "@idLetter";
        const string IdParentColumn = "@idParent";
        const string IdOwnerColumn = "@idOwner";
        const string NewNameColumn = "@newName";
        const string NameColumn = "@name";
        const string IdWorkerColumn = "@idWorker";
        const string TypeColumn = "@type";
        const string DataColumn = "@data";

        const string Id = "id";
        const string IdParent = "idParent";
        const string Name = "name";
        const string FolderType = "type";
        const string IdOwner = "idOwner";
        const string IdSender = "idSender";
        const string Date = "date";
        const string Text = "text";
        const string SenderName = "senderName";
        const string FolderName = "folderName";
        const string IsRead = "isRead";
        const string IdWorker = "idWorker";
        const string TypeClientUI = "typeClientUI";
        const string TypeFolderService = "typeFolderService";
        const string Data = "data";

        private DatabaseService _databaseService;
        public FolderTreeService(DatabaseService _databaseService)
        {
            this._databaseService = _databaseService;
        }
        private DatabaseService DatabaseService
        {
            get { return _databaseService; }
        }
        public Guid GetOwnerId(Folder folder)
        {
            Guid parentId = folder.ParentId;

            if (Guid.Empty == parentId)
            {
                return folder.OwnerId;
            }

            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetFolderOwner, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, parentId, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetGuid(reader.GetOrdinal(IdOwner));
                        }
                        return Guid.Empty;
                    }
                }
            }
        }

        public Folder Create(Folder folder)
        {
            folder.OwnerId = GetOwnerId(folder);

            folder.Id = Guid.NewGuid();

            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpCreateFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdColumn, folder.Id, command);
                    DatabaseService.AddParameterWithValue(IdParentColumn, folder.ParentId, command);
                    DatabaseService.AddParameterWithValue(NameColumn, folder.Name, command);
                    DatabaseService.AddParameterWithValue(TypeColumn, folder.Type, command);
                    DatabaseService.AddParameterWithValue(IdOwnerColumn, folder.OwnerId, command);
                    DatabaseService.AddParameterWithValue(DataColumn, folder.Data, command);
                    command.ExecuteNonQuery();
                    return folder;
                }
            }
        }

        public void Delete(Guid folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpDeleteFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folderId, command);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Folder Update(Folder folder)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpUpdateFolder, connection))
                {
                    DatabaseService.AddParameterWithValue(IdFolderColumn, folder.Id, command);
                    DatabaseService.AddParameterWithValue(NewNameColumn, folder.Name, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Folder()
                            {
                                Id = reader.GetGuid(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                ParentId = reader.GetGuid(reader.GetOrdinal(IdParent)),
                                Type = reader.GetInt32(reader.GetOrdinal(FolderType)),
                                OwnerId = reader.GetGuid(reader.GetOrdinal(IdOwner)),
                                Data = reader.GetString(reader.GetOrdinal(Data))
                            };
                        }
                        throw new Exception($"Such folder {folder.Name} isn't exist!");
                    }
                }
            }
        }

        public string GetAllFolders()
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetAllFolders, connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var name = reader.GetString(reader.GetOrdinal(Name));
                            return name;
                        }
                        return string.Empty;
                    }
                }
            }
        }

        public IEnumerable<Folder> GetAllWorkerFolders(Guid workerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetFolders, connection))
                {
                    DatabaseService.AddParameterWithValue(IdWorkerColumn, workerId, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Folder> allFolders = new List<Folder>();
                        while (reader.Read())
                        {
                                allFolders.Add(
                               new Folder()
                               {
                                   Id = reader.GetGuid(reader.GetOrdinal(Id)),
                                   Name = reader.GetString(reader.GetOrdinal(Name)),
                                   Type = reader.GetInt32(reader.GetOrdinal(FolderType)),
                                   ParentId = reader.GetGuid(reader.GetOrdinal(IdParent)),
                                   OwnerId = reader.GetGuid(reader.GetOrdinal(IdOwner)),
                                   Data = reader.GetString(reader.GetOrdinal(Data))
                               });
                        }

                        if (allFolders.Count == 0)
                        {
                            throw new Exception("Folders are not exist");
                        }
                        return allFolders;
                    }
                }
            }
        }


        public IEnumerable<FolderType> GetAllFolderTypes()
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetAllFoldersType, connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<FolderType> allTypes = new List<FolderType>();
                        while (reader.Read())
                        {
                            var type = new FolderType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal(Id)),
                                TypeClientUI = reader.GetString(reader.GetOrdinal(TypeClientUI)),
                                TypeFolderService = reader.GetString(reader.GetOrdinal(TypeFolderService)),
                                Name = reader.GetString(reader.GetOrdinal(Name))
                            };
                            if (!string.IsNullOrEmpty(type.TypeClientUI) && !string.IsNullOrEmpty(type.TypeFolderService))
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

        public FolderType GetFolderType(int folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetFolderType, connection))
                {
                    DatabaseService.AddParameterWithValue(IdColumn, folderId, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FolderType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                TypeClientUI = reader.GetString(reader.GetOrdinal(TypeClientUI)),
                                TypeFolderService = reader.GetString(reader.GetOrdinal(TypeFolderService))
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

        private int GetFolderTypeId(Guid folderId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetFolderTypeId, connection))
                {
                    DatabaseService.AddParameterWithValue(IdColumn, folderId, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(reader.GetOrdinal(Id));
                        }
                        throw new Exception($"Such folderType {folderId} isn't exist!");
                    }
                }
            }
        }

        public IFolderService GetFolderService(Guid folderId)
        {
            int folderTypeId = GetFolderTypeId(folderId);

            FolderType folderType = GetFolderType(folderTypeId);

            return (IFolderService)Activator.CreateInstance(Type.GetType(folderType.TypeFolderService), DatabaseService);

        }
    }

}

