using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;
using Npgsql;


using ZstdSharp.Unsafe;
using Microsoft.Data.Sqlite;
using ASql.Events;
using ASql.Utils;
using System.Reflection;
using static ASql.ASqlManager;

namespace ASql
{

    public class ASqlConnection : DbConnection, IDisposable
    {
        SqlConnection _sqlConn;
        OracleConnection _oraConn;
        MySqlConnection _mysConn;
        NpgsqlConnection _posConn;
        SqliteConnection _litConn;

        
        public DBType DataBaseType { get; set; }

        public ASqlConnection()
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn = new NpgsqlConnection();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn = new SqliteConnection();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlConnection(string connectionString)
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn = new NpgsqlConnection(connectionString);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn = new SqliteConnection(connectionString);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlConnection(DBType dbType)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn = new NpgsqlConnection();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn = new SqliteConnection();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlConnection(DBType dbType, string connectionString)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn = new NpgsqlConnection(connectionString);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn = new SqliteConnection(connectionString);
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
        internal NpgsqlConnection GetPosgreSQLConn()
        {
            return _posConn;
        }
        internal SqliteConnection GetSqliteConn()
        {
            return _litConn;
        }
        public override string ConnectionString {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlConn.ConnectionString;
                    case ASqlManager.DBType.Oracle:
                        return _oraConn.ConnectionString;
                    case ASqlManager.DBType.MySql:
                        return _mysConn.ConnectionString;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posConn.ConnectionString;
                    case ASqlManager.DBType.Sqlite:
                        return _litConn.ConnectionString;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
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
                    case ASqlManager.DBType.PostgreSQL:
                        _litConn.ConnectionString = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litConn.ConnectionString = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public override string Database => GetDaTabase();
        private string GetDaTabase()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.Database;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.Database;
                case ASqlManager.DBType.MySql:
                    return _mysConn.Database;
                case ASqlManager.DBType.PostgreSQL:
                    return _posConn.Database;
                case ASqlManager.DBType.Sqlite:
                    return _litConn.Database;
                default:
                    throw new NotSupportedException();
            }
        }


        public override string DataSource => GetDataSource();
        private string GetDataSource()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.DataSource;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.DataSource;
                case ASqlManager.DBType.MySql:
                    return _mysConn.DataSource;
                case ASqlManager.DBType.PostgreSQL:
                    return _posConn.DataSource;
                case ASqlManager.DBType.Sqlite:
                    return _litConn.DataSource;
                default:
                    throw new NotSupportedException();
            }
        }
        public override string ServerVersion => GetServerVersion();
        private string GetServerVersion()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.ServerVersion;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.ServerVersion;
                case ASqlManager.DBType.MySql:
                    return _mysConn.ServerVersion;
                case ASqlManager.DBType.PostgreSQL:
                    return _posConn.ServerVersion;
                case ASqlManager.DBType.Sqlite:
                    return _litConn.ServerVersion;
                default:
                    throw new NotSupportedException();
            }
        }
        public override ConnectionState State => GetState();
        private ConnectionState GetState()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.State;
                case ASqlManager.DBType.Oracle:
                    return _oraConn.State;
                case ASqlManager.DBType.MySql:
                    return _mysConn.State;
                case ASqlManager.DBType.PostgreSQL:
                    return _posConn.State;
                case ASqlManager.DBType.Sqlite:
                    return _litConn.State;
                default:
                    throw new NotSupportedException();
            }
        }
        public override void ChangeDatabase(string databaseName)
        {
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn.ChangeDatabase(databaseName);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn.ChangeDatabase(databaseName);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Close()
        {
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn.Close();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn.Close();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public override void Open()
        {
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    _posConn.Open();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litConn.Open();
                    break;
                default:
                    throw new NotSupportedException();
            }            
        }

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            DbTransaction trans = null;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return (DbTransaction)_sqlConn.BeginTransaction(isolationLevel);
                case ASqlManager.DBType.Oracle:
                    //in oracle these are the only two IsolationLevel's permitted
                    if (isolationLevel != IsolationLevel.ReadCommitted || isolationLevel != IsolationLevel.Serializable)
                    {
                        isolationLevel = IsolationLevel.ReadCommitted;
                    }
                    trans = (DbTransaction)_oraConn.BeginTransaction(isolationLevel);
                    break;
                case ASqlManager.DBType.MySql:
                    trans = (DbTransaction)_mysConn.BeginTransaction(isolationLevel);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    trans = (DbTransaction)_posConn.BeginTransaction(isolationLevel);
                    break;
                case ASqlManager.DBType.Sqlite:
                    trans = (DbTransaction)_litConn.BeginTransaction(isolationLevel);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return trans;
        }

        protected override DbCommand CreateDbCommand()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlConn.CreateCommand();
                case ASqlManager.DBType.Oracle:
                    return _oraConn.CreateCommand();
                case ASqlManager.DBType.MySql:
                    return _mysConn.CreateCommand();
                case ASqlManager.DBType.PostgreSQL:
                    return _posConn.CreateCommand();
                case ASqlManager.DBType.Sqlite:
                    return _litConn.CreateCommand();
                default:
                    throw new NotSupportedException();
            }
        }

        public void Dispose()
        {
            switch (DataBaseType)
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
                case ASqlManager.DBType.PostgreSQL:
                    if (_posConn != null && _posConn.State != ConnectionState.Closed)
                    {
                        _posConn.Close();
                    }
                    break;
                case ASqlManager.DBType.Sqlite:
                    if (_litConn != null && _litConn.State != ConnectionState.Closed)
                    {
                        _litConn.Close();
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
