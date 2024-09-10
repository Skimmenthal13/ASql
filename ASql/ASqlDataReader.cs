using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZstdSharp.Unsafe;

namespace ASql
{
    public class ASqlDataReader : DbDataReader
    {
        SqlDataReader _sqlReader;
        OracleDataReader _oraReader;
        MySqlDataReader _mysReader;
        NpgsqlDataReader _posReader;
        SqliteDataReader _litReader;
        
        public override object this[int ordinal] => GetThis(ordinal);
        private object GetThis(int ordinal) 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader[ordinal];
                case ASqlManager.DBType.Oracle:
                    return _oraReader[ordinal];
                case ASqlManager.DBType.MySql:
                    return _mysReader[ordinal];
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader[ordinal];
                case ASqlManager.DBType.Sqlite:
                    return _litReader[ordinal];
                default:
                    throw new NotSupportedException();
            }
        }
        public override object this[string name] => GetThis(name);
        private object GetThis(string name)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader[name];
                case ASqlManager.DBType.Oracle:
                    return _oraReader[name];
                case ASqlManager.DBType.MySql:
                    return _mysReader[name];
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader[name];
                case ASqlManager.DBType.Sqlite:
                    return _litReader[name];
                default:
                    throw new NotSupportedException();
            }
        }
        public override int Depth => GetDepth();
        private int GetDepth() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.Depth;
                case ASqlManager.DBType.Oracle:
                    return _oraReader.Depth;
                case ASqlManager.DBType.MySql:
                    return _mysReader.Depth;
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.Depth;
                case ASqlManager.DBType.Sqlite:
                    return _litReader.Depth;
                default:
                    throw new NotSupportedException();
            }
        }
        public override int FieldCount => GetFieldCount();
        private int GetFieldCount()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.FieldCount;
                case ASqlManager.DBType.Oracle:
                    return _oraReader.FieldCount;
                case ASqlManager.DBType.MySql:
                    return _mysReader.FieldCount;
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.FieldCount;
                case ASqlManager.DBType.Sqlite:
                    return _litReader.FieldCount;
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool HasRows => GetHasRows();
        private bool GetHasRows()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.HasRows;
                case ASqlManager.DBType.Oracle:
                    return _oraReader.HasRows;
                case ASqlManager.DBType.MySql:
                    return _mysReader.HasRows;
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.HasRows;
                case ASqlManager.DBType.Sqlite:
                    return _litReader.HasRows;
                default:
                    throw new NotSupportedException();
            }
        }
        public override bool IsClosed => GetIsClosed();
        private bool GetIsClosed()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.IsClosed;
                case ASqlManager.DBType.Oracle:
                    return _oraReader.IsClosed;
                case ASqlManager.DBType.MySql:
                    return _mysReader.IsClosed;
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.IsClosed;
                case ASqlManager.DBType.Sqlite:
                    return _litReader.IsClosed;
                default:
                    throw new NotSupportedException();
            }
        }
        public override int RecordsAffected => GetRecordsAffected();
        private int GetRecordsAffected()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.RecordsAffected;
                case ASqlManager.DBType.Oracle:
                    return _oraReader.RecordsAffected;
                case ASqlManager.DBType.MySql:
                    return _mysReader.RecordsAffected;
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.RecordsAffected;
                case ASqlManager.DBType.Sqlite:
                    return _litReader.RecordsAffected;
                default:
                    throw new NotSupportedException();
            }
        }
        public override bool GetBoolean(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetBoolean(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetBoolean(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetBoolean(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetBoolean(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetBoolean(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override byte GetByte(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetByte(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetByte(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetByte(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetByte(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetByte(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetBytes(ordinal,dataOffset,buffer,bufferOffset,length);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
                default:
                    throw new NotSupportedException();
            }
        }

        public override char GetChar(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetChar(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetChar(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetChar(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetChar(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetChar(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
                default:
                    throw new NotSupportedException();
            }
        }

        public override string GetDataTypeName(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetDataTypeName(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetDataTypeName(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetDataTypeName(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetDataTypeName(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetDataTypeName(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override DateTime GetDateTime(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetDateTime(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetDateTime(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetDateTime(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetDateTime(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetDateTime(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override decimal GetDecimal(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetDecimal(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetDecimal(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetDecimal(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetDecimal(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetDecimal(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override double GetDouble(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetDouble(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetDouble(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetDouble(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetDouble(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetDouble(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override IEnumerator GetEnumerator()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetEnumerator();
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetEnumerator();
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetEnumerator();
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetEnumerator();
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetEnumerator();
                default:
                    throw new NotSupportedException();
            }
        }

        //[return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]
        public override Type GetFieldType(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetFieldType(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetFieldType(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetFieldType(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetFieldType(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetFieldType(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override float GetFloat(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetFloat(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetFloat(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetFloat(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetFloat(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetFloat(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override Guid GetGuid(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetGuid(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetGuid(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetGuid(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetGuid(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetGuid(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override short GetInt16(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetInt16(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetInt16(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetInt16(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetInt16(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetInt16(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override int GetInt32(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetInt32(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetInt32(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetInt32(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetInt32(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetInt32(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override long GetInt64(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetInt64(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetInt64(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetInt64(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetInt64(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetInt64(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override string GetName(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetName(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetName(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetName(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetName(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetName(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override int GetOrdinal(string name)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetOrdinal(name);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetOrdinal(name);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetOrdinal(name);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetOrdinal(name);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetOrdinal(name);
                default:
                    throw new NotSupportedException();
            }
        }

        public override string GetString(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetString(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetString(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetString(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetString(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetString(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override object GetValue(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetValue(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetValue(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetValue(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetValue(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetValue(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override int GetValues(object[] values)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.GetValues(values);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.GetValues(values);
                case ASqlManager.DBType.MySql:
                    return _mysReader.GetValues(values);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.GetValues(values);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.GetValues(values);
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool IsDBNull(int ordinal)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.IsDBNull(ordinal);
                case ASqlManager.DBType.Oracle:
                    return _oraReader.IsDBNull(ordinal);
                case ASqlManager.DBType.MySql:
                    return _mysReader.IsDBNull(ordinal);
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.IsDBNull(ordinal);
                case ASqlManager.DBType.Sqlite:
                    return _litReader.IsDBNull(ordinal);
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool NextResult()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.NextResult();
                case ASqlManager.DBType.Oracle:
                    return _oraReader.NextResult();
                case ASqlManager.DBType.MySql:
                    return _mysReader.NextResult();
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.NextResult();
                case ASqlManager.DBType.Sqlite:
                    return _litReader.NextResult();
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool Read()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlReader.Read();
                case ASqlManager.DBType.Oracle:
                    return _oraReader.Read();
                case ASqlManager.DBType.MySql:
                    return _mysReader.Read();
                case ASqlManager.DBType.PostgreSQL:
                    return _posReader.Read();
                case ASqlManager.DBType.Sqlite:
                    return _litReader.Read();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
