using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;



namespace ASql
{
    public class ASqlParameterCollection : DbParameterCollection
    {
        SqlParameterCollection _sqlPrmColl;
        OracleParameterCollection _oraPrmColl;
        public override int Count => GetCount();
        private int GetCount() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.Count;
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.Count;
                default:
                    throw new NotSupportedException();
            }
        }
        public override object SyncRoot => GetSyncRoot();
        private object GetSyncRoot() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.SyncRoot;
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.SyncRoot;
                default:
                    throw new NotSupportedException();
            }
        }
        public override int Add(object value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.Add(value);
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.Add(value);
                default:
                    throw new NotSupportedException();
            }
        }

        public override void AddRange(Array values)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.AddRange(values);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.AddRange(values);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Clear()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.Clear();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.Clear();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool Contains(object value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.Contains(value);
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.Contains(value);
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool Contains(string value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.Contains(value);
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.Contains(value);
                default:
                    throw new NotSupportedException();
            }
        }

        public override void CopyTo(Array array, int index)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.CopyTo(array,index);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.CopyTo(array,index);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override IEnumerator GetEnumerator()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.GetEnumerator();
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.GetEnumerator();
                default:
                    throw new NotSupportedException();
            }
        }

        public override int IndexOf(object value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.IndexOf(value);
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.IndexOf(value);
                default:
                    throw new NotSupportedException();
            }
        }

        public override int IndexOf(string parameterName)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl.IndexOf(parameterName);
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl.IndexOf(parameterName);
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Insert(int index, object value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.Insert(index, value);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.Insert(index, value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Remove(object value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.Remove(value);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.Remove(value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void RemoveAt(int index)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.RemoveAt(index);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.RemoveAt(index);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void RemoveAt(string parameterName)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl.RemoveAt(parameterName);
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl.RemoveAt(parameterName);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbParameter GetParameter(int index)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl[index];
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl[index];
                default:
                    throw new NotSupportedException();
            }
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlPrmColl[parameterName];
                case ASqlManager.DBType.Oracle:
                    return _oraPrmColl[parameterName];
                default:
                    throw new NotSupportedException();
            }
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl[index] = (SqlParameter)value;
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl[index] = (OracleParameter)value;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlPrmColl[parameterName] = (SqlParameter)value;
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraPrmColl[parameterName] = (OracleParameter)value;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
