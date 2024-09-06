using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ASql
{
    public class ASqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
    {
        SqlDataAdapter _sqlDataAdapter;
        OracleDataAdapter _oraDataAdapter;
        MySqlDataAdapter _mysDataAdapter;
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
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
