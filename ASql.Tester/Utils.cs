using ASql.Events;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ASql.ASqlManager;

namespace ASql.Tester
{
    internal class Utils
    {
        #region queries
        internal const string sqlConnectionString = "Data Source=NBK-437;Persist Security Info=True;Initial Catalog=test;Integrated Security=SSPI;";
        internal const string oraConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=DBWUSR;Password=DBWUSR;";
        internal const string mysConnectionString = "Server=localhost;Database=test;Uid=sa;Pwd=ASqlAdmin01;";
        internal const string posConnectionString = "Server=127.0.0.1;Port=5432;Database=test;User Id=postgres;Password=ASqlAdmin01;";
        internal const string litConnectionString = @"Data Source=c:\temp\test.db;";
        internal const string tableName = "PERSON";
        internal const string lowerTableName = "person";
        internal static string sqlCreateTable = $"CREATE TABLE {tableName} ([id] [int] IDENTITY(1,1) NOT NULL , [firstname] [nvarchar](30) NOT NULL , [lastname] [nvarchar](30) NOT NULL , [age] [int] NULL , [value] [bigint] NULL , [birthday] [datetime2] NULL , [hourly] [decimal](18,2) NULL , [localtime] [datetimeoffset] NULL , [picture] [varbinary](max) NULL , [guid] [varchar](36) NULL , [active] [tinyint] NULL , CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED (  [id] ASC ) WITH (  PAD_INDEX = OFF,   STATISTICS_NORECOMPUTE = OFF,   IGNORE_DUP_KEY = OFF,   ALLOW_ROW_LOCKS = ON,   ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY] ) ON [PRIMARY] ";
        internal static string oraCreateTable = $"CREATE TABLE {tableName} (id number(11) NOT NULL , firstname varchar2(30 char) NOT NULL , lastname varchar2(30 char) NOT NULL , age number(11) NULL , value number(12) NULL , birthday timestamp NULL , hourly decimal(18,2) NULL , localtime timestamp NULL , picture blob  NULL , guid varchar2(36) NULL , active number(37) NULL ,PRIMARY KEY (id))";
        internal static string mysCreateTable = $"CREATE TABLE `{tableName}` (`id` int(11) NOT NULL AUTO_INCREMENT , `firstname` varchar(30) NOT NULL , `lastname` varchar(30) NOT NULL , `age` int(11) NULL , `value` int(12) NULL , `birthday` datetime NULL , `hourly` decimal(18,2) NULL , `picture` longblob NULL , `guid` varchar(36) NULL , `active` tinyint NULL ,PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4";
        internal static string posCreateTable = $"CREATE TABLE {tableName} (\"id\" SERIAL PRIMARY KEY , \"firstname\" character varying(30) NOT NULL , \"lastname\" character varying(30) NOT NULL , \"age\" integer NULL , \"value\" bigint NULL , \"birthday\" timestamp without time zone NULL , \"hourly\" numeric(18,2) NULL , \"picture\" bytea NULL , \"guid\" character varying(36) NULL , \"active\" smallint NULL ) WITH (  OIDS = FALSE)";
        internal static string litCreateTable = $"CREATE TABLE IF NOT EXISTS `{tableName}` (`id` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL , `firstName` VARCHAR(30) COLLATE NOCASE NOT NULL , `lastName` VARCHAR(30) COLLATE NOCASE NOT NULL , `age` INTEGER , `value` BIGINT , `birthday` TEXT , `hourly` DECIMAL(18,2) , `picture` BLOB , `guid` VARCHAR(36) , `active` TINYINT )";
        internal static string sqlCheckTable = $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end";
        internal static string oraCheckTable = $"SELECT table_name FROM USER_TABLES WHERE table_name='{tableName}'";
        internal static string mysCheckTable = $"SELECT count(*) as ntab FROM information_schema.TABLES WHERE (TABLE_SCHEMA = 'test') AND (TABLE_NAME = '{tableName}')";
        internal static string posCheckTable = $"SELECT table_name FROM information_schema.tables WHERE table_schema='public' and table_name='{lowerTableName}'";
        internal static string litCheckTable = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'";
        internal static string sqlDropTable = $"DROP TABLE {tableName}";
        internal static string oraDropTable = $"DROP TABLE {tableName}";
        internal static string mysDropTable = $"DROP TABLE {tableName}";
        internal static string posDropTable = $"DROP TABLE {tableName}";
        internal static string litDropTable = $"DROP TABLE {tableName}";
        internal static string sqlInsertRow = $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,picture,guid,active) VALUES (@firstname,@lastname,@age,@value,@birthday,@hourly,@picture,@guid,@active)";
        internal static string oraInsertRow = $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,picture,guid,active) VALUES (:firstname,:lastname,:age,:value,:birthday,:hourly,:picture,:guid,:active)";
        internal static string mysInsertRow = $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,picture,guid,active) VALUES (?,?,?,?,?,?,?,?,?)";
        internal static string posInsertRow = $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,picture,guid,active) VALUES (@firstname,@lastname,@age,@value,@birthday,@hourly,@picture,@guid,@active)";
        internal static string litInsertRow = $"INSERT INTO {tableName} (firstname,lastname,age,value,birthday,hourly,picture,guid,active) VALUES (@firstname,@lastname,@age,@value,@birthday,@hourly,@picture,@guid,@active)";
        internal static string sqlSelectFirstName = $"select firstname from {tableName} where lastname = @lastname";
        internal static string oraSelectFirstName = $"select firstname from {tableName} where lastname = :lastname";
        internal static string mysSelectFirstName = $"select firstname from {tableName} where lastname = ?";
        internal static string posSelectFirstName = $"select firstname from {tableName} where lastname = :lastname";
        internal static string litSelectFirstName = $"select firstname from {tableName} where lastname = @lastname";
        internal static string sqlSelectStar = $"select * from {tableName}";
        internal static string oraSelectStar = $"select * from {tableName}";
        internal static string mysSelectStar = $"select * from {tableName}";
        internal static string posSelectStar = $"select * from {tableName}";
        internal static string litSelectStar = $"select * from {tableName}";
        #endregion
        internal static byte[] _FileBytes = File.ReadAllBytes("./headshot.png");
        #region events
        
        internal static void Cmd_OnGenericQueryEnd(object sender, GenericQueryEndEventArgs e)
        {
            Console.WriteLine(e.Method+" "+e.Query +" "+ e.TotalMilliseconds);
        }

        #endregion
        #region SingleDataBasesMethods
        public static int CreateTable(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlCreateTable;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraCreateTable;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysCreateTable;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posCreateTable;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litCreateTable;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            ASqlManager.DataBaseType = dBType;
            int i = 0;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {

                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                if (dBType == ASqlManager.DBType.Oracle)
                {
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, CreateSequnce(tableName));
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, CreateTrigger(tableName));
                }
            }
            return i;
        }
        public static string CheckTable(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlCheckTable;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraCheckTable;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysCheckTable;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posCheckTable;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litCheckTable;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            ASqlManager.DataBaseType = dBType;

            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                string i = "";
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                switch (ASqlManager.DataBaseType)
                {
                    case ASqlManager.DBType.Oracle:
                    case ASqlManager.DBType.PostgreSQL:
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
                    case ASqlManager.DBType.MySql:
                        using (DbDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                i = read.GetInt32(read.GetOrdinal("ntab")).ToString();
                                if (i == "1") i = "PERSON";
                            }
                        }
                        break;
                    case ASqlManager.DBType.Sqlite:
                        using (DbDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                i = read.GetString(read.GetOrdinal("name"));
                            }
                        }
                        break;
                }

                return i;
            }
        }
        public static int DropTable(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlDropTable;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraDropTable;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysDropTable;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posDropTable;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litDropTable;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            ASqlManager.DataBaseType = dBType;
            int i = 0;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                i = cmd.ExecuteNonQuery();
                if (dBType == ASqlManager.DBType.Oracle)
                {
                    ExecuteQuery(ASqlManager.DBType.Oracle, oraConnectionString, DropSequenceQuery(tableName));
                }
            }
            return i;
        }
        public static int DropTableWithRollBack(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlDropTable;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraDropTable;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysDropTable;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posDropTable;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litDropTable;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            ASqlManager.DataBaseType = dBType;
            int i = 0;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {

                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                cmd.Transaction = trans;
                i = cmd.ExecuteNonQuery();
                trans.Rollback();
            }
            return i;
        }
        public static int InsertRow(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlInsertRow;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraInsertRow;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysInsertRow;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posInsertRow;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litInsertRow;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            int r = 0;
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                cmd.OnGenericQueryEnd += Cmd_OnGenericQueryEnd;
                string paramChar = "";

                List<ASqlParameter> apc = Utils.GetASqlParametersFromScratch(paramChar);
                foreach (ASqlParameter param in apc)
                {
                    cmd.aSqlParameters.Add(param);
                }

                r = cmd.ExecuteNonQuery();
            }
            return r;
        }

        public static int InsertRowTransaction(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlInsertRow;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraInsertRow;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysInsertRow;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posInsertRow;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litInsertRow;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            int r = 0;
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                cmd.Transaction = trans;
                string paramChar = "";
                List<ASqlParameter> apc = Utils.GetASqlParametersFromScratch(paramChar);
                foreach (ASqlParameter param in apc)
                {
                    cmd.aSqlParameters.Add(param);
                }
                r = cmd.ExecuteNonQuery();
                trans.Rollback();
            }
            return r;
        }
        public static string ExecuteScalar(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlSelectFirstName;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraSelectFirstName;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysSelectFirstName;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posSelectFirstName;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litSelectFirstName;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            ASqlManager.DataBaseType = dBType;
            string name = "";
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                ASqlParameter par = new ASqlParameter();
                par.ParameterName = "lastname";
                par.DbType = DbType.String;
                par.Value = "last1";
                cmd.aSqlParameters.Add(par);
                name = (string)cmd.ExecuteScalar();
            }
            return name;
        }
        public static string ExecuteReader(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlSelectFirstName;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraSelectFirstName;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysSelectFirstName;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posSelectFirstName;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litSelectFirstName;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            string i = "";
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                ASqlParameter par = new ASqlParameter();
                par.ParameterName = "lastname";
                par.DbType = DbType.String;
                par.Value = "last1";
                cmd.aSqlParameters.Add(par);
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        i = read.GetString(read.GetOrdinal("firstname"));
                    }
                }
            }
            return i;
        }
        public static int DataAdapter(ASqlManager.DBType dBType, string ConnectionString, string sql = "")
        {
            if (string.IsNullOrEmpty(sql))
            {
                switch (dBType)
                {
                    case ASqlManager.DBType.SqlServer:
                        sql = sqlSelectStar;
                        break;
                    case ASqlManager.DBType.Oracle:
                        sql = oraSelectStar;
                        break;
                    case ASqlManager.DBType.MySql:
                        sql = mysSelectStar;
                        break;
                    case ASqlManager.DBType.PostgreSQL:
                        sql = posSelectStar;
                        break;
                    case ASqlManager.DBType.Sqlite:
                        sql = litSelectStar;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            int i = 0;
            ASqlManager.DataBaseType = dBType;
            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                ASqlDataAdapter aSqlDataAdapter = new ASqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                i = aSqlDataAdapter.Fill(ds);
            }
            return i;
        }
        #endregion
        #region MultiDatabaseTests
       
        public static string ExecuteScalar(ASqlConnection conn, string sql) 
        {
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            ASqlParameter par = new ASqlParameter(conn.DataBaseType);
            par.ParameterName = "lastname";
            par.DbType = DbType.String;
            par.Value = "last1";
            cmd.aSqlParameters.Add(par);
            string name = (string)cmd.ExecuteScalar();
            return name;
        }
        public static int ExecuteNonQuery(ASqlConnection conn, string sql)
        {
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            return cmd.ExecuteNonQuery();
        }
        
        public static string ExecuteReader(ASqlConnection conn, string sql) 
        {
            string i = "";
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            ASqlParameter par = new ASqlParameter(conn.DataBaseType);
            par.ParameterName = "lastname";
            par.DbType = DbType.String;
            par.Value = "last1";
            cmd.aSqlParameters.Add(par);
            using (DbDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    i = read.GetString(read.GetOrdinal("firstname"));
                }
            }
            return i;
        }

        
        public static int InsertRow(ASqlConnection conn, string sql, DbTransaction trans=null) 
        {
            int r = 0;
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            if(trans!=null)
                cmd.Transaction = trans;
            string paramChar = "";
            List<ASqlParameter> apc = Utils.GetASqlParametersFromScratch(conn.DataBaseType,paramChar);
            foreach (ASqlParameter param in apc)
            {
                cmd.aSqlParameters.Add(param);
            }
            r = cmd.ExecuteNonQuery();
            return r;
        }
        
        public static int DataAdapter(ASqlConnection conn, string sql) 
        {
            int i = 0;   
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            ASqlDataAdapter aSqlDataAdapter = new ASqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            i = aSqlDataAdapter.Fill(ds);
            return i;
        }
        public static string CheckTable(ASqlConnection conn, string sql) 
        {
            string table = "";
            ASqlCommand cmd = new ASqlCommand(sql, conn);
            switch (conn.DataBaseType)
            {
                case ASqlManager.DBType.Oracle:
                case ASqlManager.DBType.PostgreSQL:
                    using (DbDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            table = read.GetString(read.GetOrdinal("table_name"));
                        }
                    }
                    break;
                case ASqlManager.DBType.SqlServer:
                    bool exists = (int)cmd.ExecuteScalar() == 1;
                    if (exists) { table = tableName; }
                    break;
                case ASqlManager.DBType.MySql:
                    using (DbDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            table = read.GetInt32(read.GetOrdinal("ntab")).ToString();
                            if (table == "1") table = "PERSON";
                        }
                    }
                    break;
                case ASqlManager.DBType.Sqlite:
                    using (DbDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            table = read.GetString(read.GetOrdinal("name"));
                        }
                    }
                    break;
            }
            return table;
        }
        public static int CreateTable(ASqlConnection conn, string sql) 
        {
            int i = ExecuteNonQuery(conn, sql);
            if (conn.DataBaseType == ASqlManager.DBType.Oracle)
            {
                ExecuteNonQuery(conn, CreateSequnce(tableName));
                ExecuteNonQuery(conn, CreateTrigger(tableName));
            }
            return i;
        }
        public static int DropTable(ASqlConnection conn, string sql) 
        {
            int i = ExecuteNonQuery(conn, sql);
            if (conn.DataBaseType == ASqlManager.DBType.Oracle)
            {
                ExecuteNonQuery(conn, DropSequenceQuery(tableName));
            }
            return i;
        }
        #endregion
        #region Utilities
        public static string CreateSequnce(string tableName)
        {
            string query = $"CREATE SEQUENCE {tableName}_seq START WITH 1 INCREMENT BY 1";
            return query;
        }
        public static string CreateTrigger(string tableName)
        {
            string query = $"CREATE OR REPLACE TRIGGER {tableName}_seq_tr " +
                           $"BEFORE INSERT ON {tableName} FOR EACH ROW " +
                           "WHEN (NEW.id IS NULL) " +
                           "BEGIN " +
                           $"SELECT {tableName}_seq.NEXTVAL INTO :NEW.id FROM DUAL; " +
                           "END; ";
            return query;
        }
        public static string DropSequenceQuery(string tableName)
        {
            //string query = "DECLARE cnt NUMBER;\r\n  BEGIN\r\n    SELECT COUNT(*) INTO cnt FROM user_tables WHERE table_name = '" + SanitizeString(tableName) + "';\r\n    IF cnt <> 0 THEN\r\n      EXECUTE IMMEDIATE 'DROP TABLE " + SanitizeString(tableName) + "';\r\n    END IF;\r\n  END;";
            string query = "DROP SEQUENCE " + tableName.ToUpper() + "_SEQ" + "";
            return query;
        }
        public static void ExecuteQuery(ASqlManager.DBType dBType, string ConnectionString, string sql)
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
        static internal List<ASqlParameter> GetASqlParametersFromScratch(string paramChar) 
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
                d.Add("picture", Utils._FileBytes);
                d.Add("guid", Guid.NewGuid());
                d.Add("active", (i % 2 > 0));
            }
            return GetParametersFromkeyValuePairs(d,paramChar);
        }
        static internal List<ASqlParameter> GetASqlParametersFromScratch(DBType databaseType, string paramChar)
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
                d.Add("picture", Utils._FileBytes);
                d.Add("guid", Guid.NewGuid());
                d.Add("active", (i % 2 > 0));
            }
            return GetParametersFromkeyValuePairs(databaseType, d, paramChar);
        }
        static internal List<ASqlParameter> GetParametersFromkeyValuePairs(Dictionary<string, object> keyValuePairs, string paramChar)
        {
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
                    int val = ((bool)currKvp.Value ? 1 : 0); 
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
        static internal List<ASqlParameter> GetParametersFromkeyValuePairs(DBType databaseType, Dictionary<string, object> keyValuePairs, string paramChar)
        {
            string vals = "";
            List<ASqlParameter> prm = new List<ASqlParameter>();
            foreach (KeyValuePair<string, object> currKvp in keyValuePairs)
            {

                if (currKvp.Value is DateTime
                    || currKvp.Value is DateTime?)
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is DateTimeOffset
                    || currKvp.Value is DateTimeOffset?)
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is int
                    || currKvp.Value is long
                    || currKvp.Value is decimal)
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is bool)
                {
                    int val = ((bool)currKvp.Value ? 1 : 0);
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = val;
                    prm.Add(para);
                }
                else if (currKvp.Value is byte[])
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is string)
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is Guid)
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value.ToString();
                    prm.Add(para);
                }
                else
                {
                    ASqlParameter para = new ASqlParameter(databaseType);
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }


            }
            return prm;
        }
        #endregion
    }
}
