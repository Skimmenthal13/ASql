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
    public class ASqlTransaction : DbTransaction
    {
        SqlTransaction _sqlTran;
        OracleTransaction _oraTran;
        public override IsolationLevel IsolationLevel => GetIsolationLevel();
        private IsolationLevel GetIsolationLevel() 
        {
            switch (ASqlManager.DataBaseType)
            {
                case ASqlManager.DBType.SqlServer:
                    return _sqlTran.IsolationLevel;
                case ASqlManager.DBType.Oracle:
                    return _oraTran.IsolationLevel;
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
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
