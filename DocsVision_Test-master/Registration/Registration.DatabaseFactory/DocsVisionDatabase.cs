using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectManager.SearchModel;
using DocsVision.Platform.ObjectManager.Metadata;
using System.Reflection;

namespace Registration.DatabaseFactory
{
    public class DocsVisionDatabase //: DatabaseService
    {
        //public override IDatabaseConnection CreateConnection()
        //{
        //    SessionManager manager = SessionManager.CreateInstance();
        //    manager.Connect("http://localhost/dv/StorageServer/StorageServerService.asmx", "DV");
        //    UserSession session = manager.CreateSession();
        //    return new DocsVisionDatabaseConnection(manager, session.Id);
        //}

        //private void sp_GetWorker()
        //{
        //    SessionManager manager = SessionManager.CreateInstance();
        //    manager.Connect(connectionString, "DV");
        //    UserSession session = manager.CreateSession();
        //    SectionQuery searchQuery = session.CreateSectionQuery();
            
        //    searchQuery.ConditionGroup.Conditions.AddNew("id", FieldType.String, ConditionOperation.Equals, "11");
        //}
       

        //public override IDatabaseCommand CreateStoredProcCommand(string procName, IDatabaseConnection connection)
        //{
        //    var method = typeof(DocsVisionDatabase).GetMethod(procName);
                     
        //    SqlCommand command = new SqlCommand();
        //    command.CommandText = procName;
        //    command.Connection = ((SqlDatabaseConnection)connection).SqlConnection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    return (new SqlDatabaseCommand(command));
        //}

        //public  IDataParameter CreateParameter(string parameterName, object parameterValue)
        //{
        //    return new SqlParameter(parameterName, parameterValue);
        //}

        //public override void AddParameterWithValue(string parameterName, object parameterValue, IDatabaseCommand command)
        //{
        //    ((SqlDatabaseCommand)command).SqlCommand.Parameters.Add(CreateParameter(parameterName, parameterValue));
        //}

    }
}
