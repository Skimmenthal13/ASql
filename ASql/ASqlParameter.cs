using MySql.Data.MySqlClient;
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
    public class ASqlParameter : DbParameter
    {
        SqlParameter _sqlPrm;
        OracleParameter _oraPrm;
        MySqlParameter _mysPrm;
        internal ASqlParameterCollection? ParameterCollection { get; set; }
        public ASqlParameter() 
        {
            switch (ASqlManager.DataBaseType)
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
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, object value)
        {
            switch (ASqlManager.DataBaseType)
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
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type)
        {
            switch (ASqlManager.DataBaseType)
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
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type, int size)
        {
            switch (ASqlManager.DataBaseType)
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
                default:
                    throw new NotSupportedException();
            }
        }
        public ASqlParameter(string parameterName, DbType type, int size, string sourceColumn)
        {
            switch (ASqlManager.DataBaseType)
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
                default:
                    throw new NotSupportedException();
            }
        }
        public override DbType DbType 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.DbType;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.DbType;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.DbType;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        
        public override ParameterDirection Direction 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Direction;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Direction;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Direction;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override bool IsNullable 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.IsNullable;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.IsNullable;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.IsNullable;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override string ParameterName 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.ParameterName;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.ParameterName;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.ParameterName;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override int Size 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Size;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Size;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Size;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override string SourceColumn 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.SourceColumn;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.SourceColumn;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.SourceColumn;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override bool SourceColumnNullMapping 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.SourceColumnNullMapping;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.SourceColumnNullMapping;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }
        public override object Value 
        {
            get {
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        return _sqlPrm.Value;
                    case ASqlManager.DBType.Oracle:
                        return _oraPrm.Value;
                    case ASqlManager.DBType.MySql:
                        return _mysPrm.Value;
                    default:
                        throw new NotSupportedException();
                }
            }
            set {
                switch (ASqlManager.DataBaseType)
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
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public string NormalizedParameterName { get; internal set; }

        public override void ResetDbType()
        {
            switch (ASqlManager.DataBaseType)
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
