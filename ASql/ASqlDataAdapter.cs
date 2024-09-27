using ASql.Events;
using ASql.Utils;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using static ASql.ASqlManager;


namespace ASql
{
    public class ASqlDataAdapter : DbDataAdapter, IDbDataAdapter
    {
        readonly SqlDataAdapter _sqlDataAdapter = null;
        readonly OracleDataAdapter _oraDataAdapter = null;
        readonly MySqlDataAdapter _mysDataAdapter = null;
        readonly NpgsqlDataAdapter _posDataAdapter = null;
        readonly SqliteDataAdapter _litDataAdapter = null;

        public DBType DataBaseType { get; set; }

        readonly ASqlCommand _cmd = null;

        public ASqlDataAdapter() 
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
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
        public ASqlDataAdapter(DBType dBType)
        {
            DataBaseType = dBType;
            switch (DataBaseType)
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
            DataBaseType = command.DataBaseType;
            _cmd = command;
            switch (DataBaseType)
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
        
        public new int Fill(DataSet dataSet)
        {
            DateTime startTime = DateTime.Now;
            int res = 0;
            string query = _cmd.CommandText;
            switch (DataBaseType)
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
            DateTime now = DateTime.Now;
            double totalMs = (now - startTime).TotalMilliseconds;
            OnGenericQueryEnd?.Invoke(this, new GenericQueryEndEventArgs {Method=ReflectionHelper.GetMethodFullName(MethodBase.GetCurrentMethod()), Query = query, TotalMilliseconds = totalMs, aSqlParameters = _cmd.aSqlParameters });
            return res;
        }
        public event EventHandler<GenericQueryEndEventArgs> OnGenericQueryEnd;
    }
}
