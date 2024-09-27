using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASql.ASqlManager;

namespace ASql
{
    public class ASqlParameter : DbParameter
    {
        readonly SqlParameter _sqlPrm = null;
        readonly OracleParameter _oraPrm = null;
        readonly MySqlParameter _mysPrm = null;
        readonly NpgsqlParameter _posPrm = null;
        readonly SqliteParameter _litPrm = null;

        public DBType DataBaseType { get; set; }
        #nullable enable
        internal ASqlParameterCollection? ParameterCollection { get; set; }
        #nullable disable   
        public ASqlParameter() 
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, object value)
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type)
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, type);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type, int size)
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, (SqlDbType)type, size);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, (OracleDbType)type, size);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, (MySqlDbType)type, size);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, (NpgsqlDbType)type, size);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, (SqliteType)type, size);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type, int size, string sourceColumn)
        {
            DataBaseType = ASqlManager.DataBaseType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, (SqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, (OracleDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, (MySqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, (NpgsqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, (SqliteType)type, size, sourceColumn);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(DBType dbType)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(DBType dbType,string parameterName, object value)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, value);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(DBType dbType, string parameterName, DbType type)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, type);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, type);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(DBType dbType, string parameterName, DbType type, int size)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, (SqlDbType)type, size);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, (OracleDbType)type, size);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, (MySqlDbType)type, size);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, (NpgsqlDbType)type, size);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, (SqliteType)type, size);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(DBType dbType, string parameterName, DbType type, int size, string sourceColumn)
        {
            DataBaseType = dbType;
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm = new SqlParameter(parameterName, (SqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm = new OracleParameter(parameterName, (OracleDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm = new MySqlParameter(parameterName, (MySqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm = new NpgsqlParameter(parameterName, (NpgsqlDbType)type, size, sourceColumn);
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm = new SqliteParameter(parameterName, (SqliteType)type, size, sourceColumn);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        public override DbType DbType 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.DbType;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.DbType;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.DbType;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.DbType;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.DbType;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.DbType = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.DbType = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.DbType = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.DbType = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.DbType = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        
        public override ParameterDirection Direction 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Direction;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Direction;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Direction;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.Direction;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.Direction;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.Direction = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.Direction = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.Direction = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.Direction = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.Direction = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override bool IsNullable 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.IsNullable;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.IsNullable;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.IsNullable;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.IsNullable;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.IsNullable;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.IsNullable = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.IsNullable = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.IsNullable = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.IsNullable = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.IsNullable = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override string ParameterName 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.ParameterName;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.ParameterName;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.ParameterName;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.ParameterName;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.ParameterName;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.ParameterName = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.ParameterName = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.ParameterName = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.ParameterName = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.ParameterName = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override int Size 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Size;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Size;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Size;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.Size;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.Size;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.Size = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.Size = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.Size = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.Size = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.Size = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override string SourceColumn 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.SourceColumn;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.SourceColumn;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.SourceColumn;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.SourceColumn;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.SourceColumn;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.SourceColumn = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.SourceColumn = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.SourceColumn = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.SourceColumn = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.SourceColumn = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override bool SourceColumnNullMapping 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.SourceColumnNullMapping;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.SourceColumnNullMapping = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.SourceColumnNullMapping = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.SourceColumnNullMapping = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.SourceColumnNullMapping = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.SourceColumnNullMapping = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override object Value 
        {
            get {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Value;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Value;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Value;
                    case ASqlManager.DBType.PostgreSQL:
                        return _posPrm.Value;
                    case ASqlManager.DBType.Sqlite:
                        return _litPrm.Value;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        _sqlPrm.Value = value;
                        break;
                    case ASqlManager.DBType.Oracle:
                        _oraPrm.Value = value;
                        break;
                    case ASqlManager.DBType.MySql:
                        _mysPrm.Value = value;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        _posPrm.Value = value;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        _litPrm.Value = value;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public string NormalizedParameterName { get; internal set; }

        public override void ResetDbType()
        {
            switch (DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrm.ResetSqlDbType();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrm.ResetDbType();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysPrm.ResetDbType();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posPrm.ResetDbType();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litPrm.ResetDbType();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        internal static string NormalizeParameterName(string v)
        {
            return v;
        }
    }
}
