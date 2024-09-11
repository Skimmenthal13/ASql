using ASql.events;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASql
{
    public class ASqlCommand : DbCommand
    {
        public event EventHandler<ExecuteNonQueryEndEventArgs> OnExecuteNonQueryEnd;
        internal SqlCommand _sqlCmd;
        internal OracleCommand _oraCmd;
        internal MySqlCommand _mysCmd;
        internal NpgsqlCommand _posCmd;
        internal SqliteCommand _litCmd;
        public ASqlCommand()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlCmd = new SqlCommand();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraCmd = new OracleCommand();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysCmd = new MySqlCommand();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posCmd = new NpgsqlCommand();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litCmd = new SqliteCommand();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlCommand(string CommandText)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlCmd = new SqlCommand(CommandText);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraCmd = new OracleCommand(CommandText);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysCmd = new MySqlCommand(CommandText);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posCmd = new NpgsqlCommand(CommandText);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litCmd = new SqliteCommand(CommandText);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlCommand(string CommandText, ASqlConnection connection)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlCmd = new SqlCommand(CommandText, connection.GetSqlConn());
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraCmd = new OracleCommand(CommandText, connection.GetOracleConn());
                    break;
                case ASqlManager.DBType.MySql:
                    _mysCmd = new MySqlCommand(CommandText, connection.GetMySqlConn());
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posCmd = new NpgsqlCommand(CommandText, connection.GetPosgreSQLConn());
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litCmd = new SqliteCommand(CommandText, connection.GetSqliteConn());
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        private ASqlParameterCollection? m_parameterCollection;
        public ASqlParameterCollection aSqlParameters => m_parameterCollection ??= [];
        public override string CommandText {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.CommandText;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.CommandText;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.CommandText;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.CommandText;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.CommandText;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.CommandText = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.CommandText = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.CommandText = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.CommandText = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.CommandText = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override int CommandTimeout {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.CommandTimeout;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.CommandTimeout;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.CommandTimeout;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.CommandTimeout;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.CommandTimeout;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.CommandTimeout = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.CommandTimeout = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.CommandTimeout = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.CommandTimeout = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.CommandTimeout = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override CommandType CommandType {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.CommandType;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.CommandType;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.CommandType;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.CommandType;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.CommandType;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.CommandType = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.CommandType = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.CommandType = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.CommandType = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.CommandType = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override bool DesignTimeVisible {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.DesignTimeVisible;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.DesignTimeVisible;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.DesignTimeVisible;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.DesignTimeVisible;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.DesignTimeVisible;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.DesignTimeVisible = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.DesignTimeVisible = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.DesignTimeVisible = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.DesignTimeVisible = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.DesignTimeVisible = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override UpdateRowSource UpdatedRowSource {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.UpdatedRowSource;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.UpdatedRowSource;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.UpdatedRowSource;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.UpdatedRowSource;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.UpdatedRowSource;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.UpdatedRowSource = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.UpdatedRowSource = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.UpdatedRowSource = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.UpdatedRowSource = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.UpdatedRowSource = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        protected override DbConnection DbConnection {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.Connection;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.Connection;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.Connection;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.Connection;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.Connection;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.Connection = (SqlConnection)value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.Connection = (OracleConnection)value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.Connection = (MySqlConnection)value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.Connection = (NpgsqlConnection)value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.Connection = (SqliteConnection)value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        protected override DbParameterCollection DbParameterCollection => GetParameterCollection();
        private DbParameterCollection GetParameterCollection()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.Parameters;
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.Parameters;
                case ASqlManager.DBType.MySql:
                    return _mysCmd.Parameters;
                case ASqlManager.DBType.PostgreSQL:
                    return _posCmd.Parameters;
                case ASqlManager.DBType.Sqlite:
                    return _litCmd.Parameters;
                default:
                    throw new NotSupportedException();
            }
        }
        
        protected override DbTransaction DbTransaction {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlCmd.Transaction;
                    case ASqlManager.DBType.Oracle:
                        return _oraCmd.Transaction;
                    case ASqlManager.DBType.MySql:
                        return _mysCmd.Transaction;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posCmd.Transaction;
                    case ASqlManager.DBType.Sqlite:
                        return _litCmd.Transaction;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlCmd.Transaction = (SqlTransaction)value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraCmd.Transaction = (OracleTransaction)value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysCmd.Transaction = (MySqlTransaction)value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posCmd.Transaction = (NpgsqlTransaction)value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litCmd.Transaction = (SqliteTransaction)value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public override void Cancel()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlCmd.Cancel();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraCmd.Cancel();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysCmd.Cancel();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posCmd.Cancel();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litCmd.Cancel();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override int ExecuteNonQuery()
        {
            PopulateParameters();
            string query = "";
            DateTime startTime = DateTime.Now;
            int res = 0;
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    query = _sqlCmd.CommandText;
                    res = _sqlCmd.ExecuteNonQuery();
                    break;
                case ASqlManager.DBType.Oracle:
                    query = _oraCmd.CommandText;
                    res = _oraCmd.ExecuteNonQuery();
                    break;
                case ASqlManager.DBType.MySql:
                    query = _mysCmd.CommandText;
                    res = _mysCmd.ExecuteNonQuery();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    query = _posCmd.CommandText;
                    res = _posCmd.ExecuteNonQuery();
                    break;
                case ASqlManager.DBType.Sqlite:
                    query = _litCmd.CommandText;
                    res = _litCmd.ExecuteNonQuery();
                    break;
                default:
                    throw new NotSupportedException();
            }
            double totalMs = (DateTime.Now - startTime).TotalMilliseconds;
            OnExecuteNonQueryEnd?.Invoke(this, new ExecuteNonQueryEndEventArgs { Query = query, TotalMilliseconds = totalMs });
            return res;
            
        }
        public override object ExecuteScalar()
        {
            PopulateParameters();
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.ExecuteScalar();
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.ExecuteScalar();
                case ASqlManager.DBType.MySql:
                    return _mysCmd.ExecuteScalar();
                case ASqlManager.DBType.PostgreSQL:
                    return _posCmd.ExecuteScalar();
                case ASqlManager.DBType.Sqlite:
                    return _litCmd.ExecuteScalar();
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Prepare()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlCmd.Prepare();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraCmd.Prepare();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysCmd.Prepare();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posCmd.Prepare();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litCmd.Prepare();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.CreateParameter();
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.CreateParameter();
                case ASqlManager.DBType.MySql:
                    return _mysCmd.CreateParameter();
                case ASqlManager.DBType.PostgreSQL:
                    return _posCmd.CreateParameter();
                case ASqlManager.DBType.Sqlite:
                    return _litCmd.CreateParameter();
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            PopulateParameters();
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.ExecuteReader(behavior);
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.ExecuteReader(behavior);
                case ASqlManager.DBType.MySql:
                    return _mysCmd.ExecuteReader(behavior);
                case ASqlManager.DBType.PostgreSQL:
                    return _posCmd.ExecuteReader(behavior);
                case ASqlManager.DBType.Sqlite:
                    return _litCmd.ExecuteReader(behavior);
                default:
                    throw new NotSupportedException();
            }
        }

        private void PopulateParameters()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    foreach (ASqlParameter asqlPar in this.aSqlParameters)
                    {
                        SqlParameter sqlPar = new SqlParameter();
                        sqlPar.ParameterName = asqlPar.ParameterName;
                        sqlPar.Value = asqlPar.Value;
                        sqlPar.DbType = asqlPar.DbType;
                        sqlPar.Size = asqlPar.Size;
                        sqlPar.IsNullable = asqlPar.IsNullable;
                        sqlPar.SourceColumn = asqlPar.SourceColumn;
                        sqlPar.Direction = asqlPar.Direction;
                        this.Parameters.Add(sqlPar);
                    }
                    break;
                case ASqlManager.DBType.Oracle:
                    foreach (ASqlParameter asqlPar in this.aSqlParameters)
                    {
                        OracleParameter oraPar = new OracleParameter();
                        oraPar.ParameterName = asqlPar.ParameterName;
                        oraPar.Value = asqlPar.Value;
                        oraPar.DbType = asqlPar.DbType;
                        oraPar.Size = asqlPar.Size;
                        oraPar.IsNullable = asqlPar.IsNullable;
                        oraPar.SourceColumn = asqlPar.SourceColumn;
                        oraPar.Direction = asqlPar.Direction;
                        this.Parameters.Add(oraPar);
                    }
                    break;
                case ASqlManager.DBType.MySql:
                    foreach (ASqlParameter asqlPar in this.aSqlParameters)
                    {
                        MySqlParameter mysPar = new MySqlParameter();
                        mysPar.ParameterName = asqlPar.ParameterName;
                        mysPar.Value = asqlPar.Value;
                        mysPar.DbType = asqlPar.DbType;
                        mysPar.Size = asqlPar.Size;
                        mysPar.IsNullable = asqlPar.IsNullable;
                        mysPar.SourceColumn = asqlPar.SourceColumn;
                        mysPar.Direction = asqlPar.Direction;
                        this.Parameters.Add(mysPar);
                    }
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    foreach (ASqlParameter asqlPar in this.aSqlParameters)
                    {
                        NpgsqlParameter posPar = new NpgsqlParameter();
                        posPar.ParameterName = asqlPar.ParameterName;
                        posPar.Value = asqlPar.Value;
                        posPar.DbType = asqlPar.DbType;
                        posPar.Size = asqlPar.Size;
                        posPar.IsNullable = asqlPar.IsNullable;
                        posPar.SourceColumn = asqlPar.SourceColumn;
                        posPar.Direction = asqlPar.Direction;
                        this.Parameters.Add(posPar);
                    }
                    break;
                case ASqlManager.DBType.Sqlite:
                    foreach (ASqlParameter asqlPar in this.aSqlParameters)
                    {
                        SqliteParameter litPar = new SqliteParameter();
                        litPar.ParameterName = asqlPar.ParameterName;
                        litPar.Value = asqlPar.Value;
                        litPar.DbType = asqlPar.DbType;
                        litPar.Size = asqlPar.Size;
                        litPar.IsNullable = asqlPar.IsNullable;
                        litPar.SourceColumn = asqlPar.SourceColumn;
                        litPar.Direction = asqlPar.Direction;
                        this.Parameters.Add(litPar);
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
