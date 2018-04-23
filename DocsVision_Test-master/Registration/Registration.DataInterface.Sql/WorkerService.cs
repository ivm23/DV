using System;
using System.Collections.Generic;
using Registration.Model;
using System.Data.SqlClient;
using Registration.DatabaseFactory;
using System.Data;

namespace Registration.DataInterface.Sql
{
    public class WorkerService : IWorkerService
    {
        const string SpGetWorker = "sp_GetWorker";
        const string SpCreateWorker = "sp_CreateWorker";
        const string SpGetWorkerIdName = "sp_GetWorkerIdName";
        const string SpGetAllWorkers = "sp_GetAllWorkers";
        const string SpGetWorkerById = "sp_GetWorkerById";

        const string LoginColumn = "@login";
        const string PasswordColumn = "@password";
        const string IdColumn = "@id";
        const string NameColumn = "@name";
        const string IdWorkerColumn = "@idWorker";

        const string Password = "password";
        const string Login = "login";
        const string Id = "id";
        const string Name = "name";
        const string PrivateFolders = "privateFolders";

        private DatabaseService _databaseService;
        public WorkerService(DatabaseService _databaseService)
        {
            this._databaseService = _databaseService;
        }

        public DatabaseService DatabaseService
        {
            get { return _databaseService; }
        }

        public Worker AuthorizationWorker(string loginW, string passwordW)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetWorker, connection))
                {
                    DatabaseService.AddParameterWithValue(LoginColumn, loginW, command);
                    DatabaseService.AddParameterWithValue(PasswordColumn, passwordW, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Worker
                            {
                                Id = reader.GetGuid(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                Login = loginW,
                                Password = passwordW
                            };
                        }
                        throw new Exception($"Such worker {loginW} {passwordW} isn't exist!");
                    }
                }
            }
        }

        public Worker Create(Worker worker)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                Worker userExist = new Worker();
                worker.Id = Guid.NewGuid();
                try
                {
                    return Get(worker.Login);
                }
                catch (Exception ex)
                {


                    FolderTreeService folderTreeService = new FolderTreeService(_databaseService);
                    folderTreeService.Create(new Folder()
                    {
                        Name = PrivateFolders,
                        ParentId = Guid.Empty,
                        Type = 0,
                        OwnerId = worker.Id,
                        Data = string.Empty
                    }
                    );
                    using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpCreateWorker, connection))
                    {
                        DatabaseService.AddParameterWithValue(IdColumn, worker.Id, command);
                        DatabaseService.AddParameterWithValue(NameColumn, worker.Name, command);
                        DatabaseService.AddParameterWithValue(LoginColumn, worker.Login, command);
                        DatabaseService.AddParameterWithValue(PasswordColumn, worker.Password, command);

                        command.ExecuteNonQuery();
                        return worker;
                    }
                }
            }
        }

        public Worker Get(string loginW)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetWorkerIdName, connection))
                {
                    DatabaseService.AddParameterWithValue(Login, loginW, command);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Worker
                            {
                                Id = reader.GetGuid(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                Login = loginW
                            };
                        }
                        throw new Exception($"Such worker {loginW} isn't exist!");
                    }
                }
            }
        }

        public IEnumerable<Worker> GetAllWorkers()
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetAllWorkers, connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Worker> users = new List<Worker>();
                        while (reader.Read())
                        {
                            users.Add(new Worker
                            {
                                Id = reader.GetGuid(reader.GetOrdinal(Id)),
                                Name = reader.GetString(reader.GetOrdinal(Name)),
                                Login = reader.GetString(reader.GetOrdinal(Login))
                            }
                            );
                        }
                        return users;
                    }
                }
            }
        }

        public string GetWorkerName(Guid workerId)
        {
            using (IDbConnection connection = DatabaseService.CreateOpenConnection())
            {
                using (IDbCommand command = DatabaseService.CreateStoredProcCommand(SpGetWorkerById, connection))
                {
                    DatabaseService.AddParameterWithValue(IdWorkerColumn, workerId, command);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        Worker worker = new Worker();
                        if (reader.Read())
                        {
                            worker.Name = reader.GetString(reader.GetOrdinal(Name));
                            worker.Login = reader.GetString(reader.GetOrdinal(Login));
                            worker.Password = reader.GetString(reader.GetOrdinal(Password));
                            worker.Id = workerId;
                        }
                        return worker.ToString();
                    }
                }
            }
        }

    }
}

