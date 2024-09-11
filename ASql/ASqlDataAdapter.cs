using ASql.Events;
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

        ASqlCommand _cmd;

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
            _cmd = command;
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
            DateTime startTime = DateTime.Now;
            int res = 0;
            string query = _cmd.CommandText;
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                       res = _sqlDataAdapter.Fill(dataSet);
                    break;
                case ASqlManager.DBType.Oracle:
                       res = _oraDataAdapter.Fill(dataSet);
                    break;
                case ASqlManager.DBType.MySql:
                       res = _mysDataAdapter.Fill(dataSet);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                       res = _posDataAdapter.Fill(dataSet);
                    break;
                case ASqlManager.DBType.Sqlite:
                       res = _litDataAdapter.Fill(dataSet);
                    break;
                default:
                    throw new NotSupportedException();
            }
            double totalMs = (DateTime.Now - startTime).TotalMilliseconds;
            OnDataAdapterFillEnd?.Invoke(this, new DataAdapterFillEndEventArgs { Query = query, TotalMilliseconds = totalMs, aSqlParameters = _cmd.aSqlParameters });
            OnGenericQueryEnd?.Invoke(this, new GenericQueryEndEventArgs { Query = query, TotalMilliseconds = totalMs, aSqlParameters = _cmd.aSqlParameters });
            return res;
        }
        public event EventHandler<DataAdapterFillEndEventArgs> OnDataAdapterFillEnd;
        public event EventHandler<GenericQueryEndEventArgs> OnGenericQueryEnd;
    }
}
