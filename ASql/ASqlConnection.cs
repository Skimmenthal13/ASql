using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace ASql
{

    public class ASqlConnection : DbConnection, IDisposable
    {
        SqlConnection _sqlConn;
        OracleConnection _oraConn;
        public ASqlConnection()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlConn = new SqlConnection();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraConn = new OracleConnection();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlConnection(string connectionString)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlConn = new SqlConnection(connectionString);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraConn = new OracleConnection(connectionString);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        internal OracleConnection GetOracleConn() 
        {
            return _oraConn;
        }
        internal SqlConnection GetSqlConn()
        {
            return _sqlConn;
        }
        public override string ConnectionString {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlConn.ConnectionString;
                    case ASqlManager.DBType.Oracle:
                        return _oraConn.ConnectionString;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlConn.ConnectionString = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraConn.ConnectionString = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public override string Database => GetDaTabase();
        private string GetDaTabase()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.Database;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.Database;
                default:
                    throw new NotSupportedException();
            }
        }


        public override string DataSource => GetDataSource();
        private string GetDataSource()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.DataSource;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.DataSource;
                default:
                    throw new NotSupportedException();
            }
        }
        public override string ServerVersion => GetServerVersion();
        private string GetServerVersion()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.ServerVersion;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.ServerVersion;
                default:
                    throw new NotSupportedException();
            }
        }
        public override ConnectionState State => GetState();
        private ConnectionState GetState()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.State;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.State;
                default:
                    throw new NotSupportedException();
            }
        }
        public override void ChangeDatabase(string databaseName)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlConn.ChangeDatabase(databaseName);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraConn.ChangeDatabase(databaseName);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Close()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlConn.Close();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraConn.Close();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public override void Open()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlConn.Open();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraConn.Open();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return (DbTransaction)_sqlConn.BeginTransaction(isolationLevel);
                case ASqlManager.DBType.Oracle:
                    //in oracle these are the only two IsolationLevel's permitted
                    if (isolationLevel != IsolationLevel.ReadCommitted || isolationLevel != IsolationLevel.Serializable)
                    {
                        isolationLevel = IsolationLevel.ReadCommitted;
                    }
                    return (DbTransaction)_oraConn.BeginTransaction(isolationLevel);
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.CreateCommand();
                case ASqlManager.DBType.Oracle:
                    return _oraConn.CreateCommand();
                default:
                    throw new NotSupportedException();
            }
        }

        public void Dispose()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    if (_sqlConn != null && _sqlConn.State != ConnectionState.Closed) 
                    {
                        _sqlConn.Close();
                    }
                    break;
                case ASqlManager.DBType.Oracle:
                    if (_oraConn != null && _oraConn.State != ConnectionState.Closed)
                    {
                        _oraConn.Close();
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
