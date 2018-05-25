using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Registration.DatabaseFactory
{
    public class MsSqlDatabase : DatabaseService
    {
        //   private IDbCommand _command;
        public override IDatabaseConnection CreateConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return new SqlDatabaseConnection(connection);
        }

        private IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        private IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }

        public override IDatabaseCommand CreateStoredProcCommand(string procName, IDatabaseConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = ((SqlDatabaseConnection)connection).SqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            return (new SqlDatabaseCommand(command));
        }

        private IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public override void AddParameterWithValue(string parameterName, object parameterValue, IDatabaseCommand command)
        {
            ((SqlDatabaseCommand)command).SqlCommand.Parameters.Add(CreateParameter(parameterName, parameterValue));
        }

    }
}
