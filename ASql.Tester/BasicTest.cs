using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASql.Tester
{
    [TestClass]
    public class BasicTest
    {
        const string oraConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=DBWUSR;Password=DBWUSR;";
        const string sqlConnectionString = "Data Source=NBK-437;Persist Security Info=True;Initial Catalog=test;Integrated Security=SSPI;";
        const string tableName = "PERSON";
        static byte[] _FileBytes = File.ReadAllBytes("./headshot.png");

        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"CREATE TABLE {tableName} ([id] [int] IDENTITY(1,1) NOT NULL , [firstname] [nvarchar](30) NOT NULL , [lastname] [nvarchar](30) NOT NULL , [age] [int] NULL , [value] [bigint] NULL , [birthday] [datetime2] NULL , [hourly] [decimal](18,2) NULL , [localtime] [datetimeoffset] NULL , [picture] [varbinary](max) NULL , [guid] [varchar](36) NULL , [active] [tinyint] NULL , CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED (  [id] ASC ) WITH (  PAD_INDEX = OFF,   STATISTICS_NORECOMPUTE = OFF,   IGNORE_DUP_KEY = OFF,   ALLOW_ROW_LOCKS = ON,   ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY] ) ON [PRIMARY] ")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"CREATE TABLE {tableName} (id number(11) NOT NULL , firstname varchar2(30 char) NOT NULL , lastname varchar2(30 char) NOT NULL , age number(11) NULL , value number(12) NULL , birthday timestamp NULL , hourly decimal(18,2) NULL , localtime timestamp NULL , picture blob  NULL , guid varchar2(36) NULL , active number(37) NULL ,PRIMARY KEY (id))")]
        [TestMethod]
        public void CreateTableTest(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                if (dBType == ASqlManager.DBType.Oracle) 
                {
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, CreateSequnce(tableName));
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, CreateTrigger(tableName));
                }
                Assert.AreEqual(-1, i);
            }
        }
        public string CreateSequnce(string tableName)
        {
            string query = $"CREATE SEQUENCE {tableName}_seq START WITH 1 INCREMENT BY 1";
            return query;
        }
        public string CreateTrigger(string tableName)
        {
            string query = $"CREATE OR REPLACE TRIGGER {tableName}_seq_tr " +
                           "BEFORE INSERT ON person FOR EACH ROW " +
                           "WHEN (NEW.id IS NULL) " +
                           "BEGIN " +
                           "SELECT person_seq.NEXTVAL INTO :NEW.id FROM DUAL; " +
                           "END; ";
            return query;
        }
        public string DropSequenceQuery(string tableName)
        {
            //string query = "DECLARE cnt NUMBER;\r\n  BEGIN\r\n    SELECT COUNT(*) INTO cnt FROM user_tables WHERE table_name = '" + SanitizeString(tableName) + "';\r\n    IF cnt <> 0 THEN\r\n      EXECUTE IMMEDIATE 'DROP TABLE " + SanitizeString(tableName) + "';\r\n    END IF;\r\n  END;";
            string query = "DROP SEQUENCE " + tableName.ToUpper() + "_SEQ" + "";
            return query;
        }
        public void ExecuteQuery(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                Assert.AreEqual(-1, i);
            }
        }

        [DataRow(ASqlManager.DBType.SqlServer,sqlConnectionString, $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"SELECT table_name FROM USER_TABLES WHERE table_name='{tableName}'")]
        [TestMethod]
        public void CheckTable(ASqlManager.DBType dBType,string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                string i = "";
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                switch (ASqlManager.DataBaseType) 
                {
                    case ASqlManager.DBType.Oracle:
                        using (DbDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                i = read.GetString(read.GetOrdinal("table_name"));
                            }
                        }
                        break;
                    case ASqlManager.DBType.SqlServer:
                        bool exists = (int)cmd.ExecuteScalar() == 1;
                        if (exists) { i = tableName; }
                        break;
                }
                
                Assert.AreEqual("PERSON", i.ToUpper());
            }
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"DROP TABLE {tableName}")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"DROP TABLE {tableName}")]
        [TestMethod]
        public void DropTable(ASqlManager.DBType dBType, string ConnectionString, string sql) 
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                if (dBType == ASqlManager.DBType.Oracle)
                {
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, DropSequenceQuery(tableName));
                }
                Assert.AreEqual(-1, i);
            }
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"DROP TABLE {tableName}")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"DROP TABLE {tableName}")]
        [TestMethod]
        public void DropTableWithRollBack(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                cmd.Transaction = trans;
                i = cmd.ExecuteNonQuery();
                trans.Rollback();
                Assert.AreEqual(-1, i);
            }
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"select firstname from {tableName} where lastname = @lastname")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"select firstname from {tableName} where lastname = :lastname")]
        [TestMethod]
        public void ExecuteScalar(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                string name = "";
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                ASqlParameter par = new ASqlParameter();
                par.ParameterName = "lastname";
                par.DbType = DbType.String;
                par.Value = "last1";
                cmd.aSqlParameters.Add(par);
                name = (string)cmd.ExecuteScalar();
                
                Assert.AreEqual("first1", name);
            }
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,localtime,picture,guid,active) VALUES (@firstname,@lastname,@age,@value,@birthday,@hourly,@localtime,@picture,@guid,@active)")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,localtime,picture,guid,active) VALUES (:firstname,:lastname,:age,:value,:birthday,:hourly,:localtime,:picture,:guid,:active)")]
        [TestMethod]
        public void InsertRow(ASqlManager.DBType dBType, string ConnectionString, string sql) 
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            for (int i = 1; i < 20; i++)
            {
                d.Add("firstname", "first" + i);
                d.Add("lastname", "last" + i);
                d.Add("age", i);
                d.Add("value", i * 1000);
                d.Add("birthday", DateTime.Now);
                d.Add("hourly", 123.456);
                d.Add("localtime", new DateTimeOffset(2021, 4, 14, 01, 02, 03, new TimeSpan(7, 0, 0)));
                d.Add("picture", _FileBytes);
                d.Add("guid", Guid.NewGuid());
                d.Add("active", (i % 2 > 0));
            }
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                string paramChar = "";
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        paramChar = "";
                        break;
                    case ASqlManager.DBType.Oracle:
                        paramChar = ":";
                        break;
                    default:
                        throw new NotSupportedException();
                }
                List<ASqlParameter> apc = GetParametersFromkeyValuePairs(d,paramChar);
                foreach (ASqlParameter param in apc)
                {
                    cmd.aSqlParameters.Add(param);
                }
                
                i = cmd.ExecuteNonQuery();
                Assert.AreEqual(1, i);
            }
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,localtime,picture,guid,active) VALUES (@firstname,@lastname,@age,@value,@birthday,@hourly,@localtime,@picture,@guid,@active)")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,localtime,picture,guid,active) VALUES (:firstname,:lastname,:age,:value,:birthday,:hourly,:localtime,:picture,:guid,:active)")]
        [TestMethod]
        public void InsertRowTransaction(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            for (int i = 1; i < 2; i++)
            {
                d.Add("firstname", "first" + i);
                d.Add("lastname", "last" + i);
                d.Add("age", i);
                d.Add("value", i * 1000);
                d.Add("birthday", DateTime.Now);
                d.Add("hourly", 123.456);
                d.Add("localtime", new DateTimeOffset(2021, 4, 14, 01, 02, 03, new TimeSpan(7, 0, 0)));
                d.Add("picture", _FileBytes);
                d.Add("guid", Guid.NewGuid());
                d.Add("active", (i % 2 > 0));
            }
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                cmd.Transaction = trans;
                
                string paramChar = "";
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.SqlServer:
                        paramChar = "";
                        break;
                    case ASqlManager.DBType.Oracle:
                        paramChar = ":";
                        break;
                    default:
                        throw new NotSupportedException();
                }
                List<ASqlParameter> apc = GetParametersFromkeyValuePairs(d, paramChar);
                foreach (ASqlParameter param in apc)
                {
                    cmd.aSqlParameters.Add(param);
                }

                i = cmd.ExecuteNonQuery();
                trans.Commit();
                Assert.AreEqual(1, i);
            }
        }
        internal List<ASqlParameter> GetParametersFromkeyValuePairs(Dictionary<string, object> keyValuePairs, string paramChar)
        {
            string vals = "";
            List<ASqlParameter> prm = new List<ASqlParameter>();
            foreach (KeyValuePair<string, object> currKvp in keyValuePairs)
            {

                if (currKvp.Value is DateTime
                    || currKvp.Value is DateTime?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is DateTimeOffset
                    || currKvp.Value is DateTimeOffset?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is int
                    || currKvp.Value is long
                    || currKvp.Value is decimal)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is bool)
                {
                    string val = ((bool)currKvp.Value ? "1" : "0");
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = val;
                    prm.Add(para);
                }
                else if (currKvp.Value is byte[])
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is string)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is Guid)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value.ToString();
                    prm.Add(para);
                }
                else
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }


            }
            return prm;
        }
        [DataRow(ASqlManager.DBType.SqlServer, sqlConnectionString, $"select * from {tableName}")]
        [DataRow(ASqlManager.DBType.Oracle, oraConnectionString, $"select * from {tableName}")]
        [TestMethod]
        public void SqlDataAdapterTester(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                ASqlDataAdapter aSqlDataAdapter = new ASqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                i = aSqlDataAdapter.Fill(ds);
                Assert.AreEqual (1, i);
            }
        }
    }
}