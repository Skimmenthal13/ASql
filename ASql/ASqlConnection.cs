using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;

namespace ASql
{

    public class ASqlConnection : DbConnection, IDisposable
    {
        SqlConnection _sqlConn;
        OracleConnection _oraConn;
        MySqlConnection _mysConn;
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
                case ASqlManager.DBType.MySql:
                    _mysConn = new MySqlConnection();
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
                case ASqlManager.DBType.MySql:
                    _mysConn = new MySqlConnection(connectionString);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        internal SqlConnection GetSqlConn()
        {
            return _sqlConn;
        }
        internal OracleConnection GetOracleConn() 
        {
            return _oraConn;
        }
        internal MySqlConnection GetMySqlConn()
        {
            return _mysConn;
        }
        public override string ConnectionString {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlConn.ConnectionString;
                    case ASqlManager.DBType.Oracle:
                        return _oraConn.ConnectionString;
                    case ASqlManager.DBType.MySql:
                        return _mysConn.ConnectionString;
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
                    case ASqlManager.DBType.MySql:
                        _mysConn.ConnectionString = value;
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
                case ASqlManager.DBType.MySql:
                    return _mysConn.Database;
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
                case ASqlManager.DBType.MySql:
                    return _mysConn.DataSource;
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
                case ASqlManager.DBType.MySql:
                    return _mysConn.ServerVersion;
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
                case ASqlManager.DBType.MySql:
                    return _mysConn.State;
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
                case ASqlManager.DBType.MySql:
                    _mysConn.ChangeDatabase(databaseName);
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
                case ASqlManager.DBType.MySql:
                    _mysConn.Close();
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
                case ASqlManager.DBType.MySql:
                    _mysConn.Open();
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
                case ASqlManager.DBType.MySql:
                    return (DbTransaction)_mysConn.BeginTransaction(isolationLevel);
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
                case ASqlManager.DBType.MySql:
                    return _mysConn.CreateCommand();
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
                case ASqlManager.DBType.MySql:
                    if (_mysConn != null && _mysConn.State != ConnectionState.Closed)
                    {
                        _mysConn.Close();
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
