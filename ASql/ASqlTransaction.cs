using ASql.Events;
using ASql.Utils;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASql
{
    public class ASqlTransaction : DbTransaction
    {
        readonly SqlTransaction _sqlTran = null;
        readonly OracleTransaction _oraTran = null;
        readonly MySqlTransaction _mysTran = null;
        readonly NpgsqlTransaction _posTran = null;
        readonly SqliteTransaction _litTran = null;
        public override IsolationLevel IsolationLevel => GetIsolationLevel();
        private IsolationLevel GetIsolationLevel() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlTran.IsolationLevel;
                case ASqlManager.DBType.Oracle:
                    return _oraTran.IsolationLevel;
                case ASqlManager.DBType.MySql:
                    return _mysTran.IsolationLevel;
                case ASqlManager.DBType.PostgreSQL:
                    return _posTran.IsolationLevel;
                case ASqlManager.DBType.Sqlite:
                    return _litTran.IsolationLevel;
                default:
                    throw new NotSupportedException();
            }
        }
        protected override DbConnection DbConnection => GetDbConnection();
        private DbConnection GetDbConnection()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlTran.Connection;
                case ASqlManager.DBType.Oracle:
                    return _oraTran.Connection;
                case ASqlManager.DBType.MySql:
                    return _mysTran.Connection;
                case ASqlManager.DBType.PostgreSQL:
                    return _posTran.Connection;
                case ASqlManager.DBType.Sqlite:
                    return _litTran.Connection;
                default:
                    throw new NotSupportedException();
            }
        }
        public override void Commit()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlTran.Commit();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraTran.Commit();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysTran.Commit();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posTran.Commit();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litTran.Commit();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Rollback()
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    _sqlTran.Rollback();
                    break;
                case ASqlManager.DBType.Oracle:
                    _oraTran.Rollback();
                    break;
                case ASqlManager.DBType.MySql:
                    _mysTran.Rollback();
                    break;
                case ASqlManager.DBType.PostgreSQL:
                    _posTran.Rollback();
                    break;
                case ASqlManager.DBType.Sqlite:
                    _litTran.Rollback();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
