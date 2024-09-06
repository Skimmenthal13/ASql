using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace ASql
{
    public class ASqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
    {
        SqlDataAdapter _sqlDataAdapter;
        OracleDataAdapter _oraDataAdapter;
        MySqlDataAdapter _mysDataAdapter;
        NpgsqlDataAdapter _posDataAdapter;
        SqliteDataAdapter _litDataAdapter;
        public ASqlDataAdapter() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlDataAdapter = new SqlDataAdapter();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraDataAdapter = new OracleDataAdapter();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysDataAdapter = new MySqlDataAdapter();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posDataAdapter = new NpgsqlDataAdapter();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litDataAdapter = new SqliteDataAdapter();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlDataAdapter(ASqlCommand command) 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlDataAdapter = new SqlDataAdapter(command._sqlCmd);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraDataAdapter = new OracleDataAdapter(command._oraCmd);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysDataAdapter = new MySqlDataAdapter(command._mysCmd);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posDataAdapter = new NpgsqlDataAdapter(command._posCmd);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litDataAdapter = new SqliteDataAdapter(command._litCmd);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlDataAdapter(string selectCommandText, ASqlConnection selectConnection) 
        {

        }
        public ASqlDataAdapter(string selectCommandText, string selectConnectionString)
        {

        }

        public int Fill(DataSet dataSet)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                       return _sqlDataAdapter.Fill(dataSet);
                case ASqlManager.DBType.Oracle:
                        return _oraDataAdapter.Fill(dataSet);
                case ASqlManager.DBType.MySql:
                    return _mysDataAdapter.Fill(dataSet);
                case ASqlManager.DBType.PostgreSQL:
                    return _posDataAdapter.Fill(dataSet);
                case ASqlManager.DBType.Sqlite:
                    return _litDataAdapter.Fill(dataSet);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
