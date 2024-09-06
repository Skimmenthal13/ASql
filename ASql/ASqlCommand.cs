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
        internal SqlCommand _sqlCmd;
        internal OracleCommand _oraCmd;
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
                default:
                    throw new NotSupportedException();
            }
        }

        public override int ExecuteNonQuery()
        {
            PopulateParameters();
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.ExecuteNonQuery();
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.ExecuteNonQuery();
                default:
                    throw new NotSupportedException();
            }
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
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlCmd.ExecuteReader(behavior);
                case ASqlManager.DBType.Oracle:
                    return _oraCmd.ExecuteReader(behavior);
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
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
