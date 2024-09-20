using ASql.Events;
using Google.Protobuf.WellKnownTypes;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;


using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace ASql.Tester
{
    [TestClass]
    public class BasicTest
    {
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void CreateTableTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.CreateTable(dBType, ConnectionString);
            if (ASqlManager.DataBaseType == ASqlManager.DBType.MySql || ASqlManager.DataBaseType == ASqlManager.DBType.Sqlite) { Assert.AreEqual(0, i); }
            else { Assert.AreEqual(-1, i); }

        }
        
        

        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void CheckTableTest(ASqlManager.DBType dBType, string ConnectionString, string sql)
        {
            string i = Utils.CheckTable(dBType, ConnectionString, sql);
            Assert.AreEqual("PERSON", i.ToUpper());
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void DropTableTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.DropTable(dBType, ConnectionString);
            if (ASqlManager.DataBaseType == ASqlManager.DBType.MySql || ASqlManager.DataBaseType == ASqlManager.DBType.Sqlite) { Assert.AreEqual(0, i); }
            else { Assert.AreEqual(-1, i); }
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void DropTableWithRollBackTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.DropTableWithRollBack(dBType, ConnectionString);
            if (ASqlManager.DataBaseType == ASqlManager.DBType.MySql || ASqlManager.DataBaseType == ASqlManager.DBType.Sqlite) { Assert.AreEqual(0, i); }
            else { Assert.AreEqual(-1, i); }
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void InsertRowTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.InsertRow(dBType, ConnectionString);
            Assert.AreEqual(1, i);
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void InsertRowTransactionTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.InsertRowTransaction(dBType, ConnectionString);
            Assert.AreEqual(1, i);
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void ExecuteScalarTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            string name = Utils.ExecuteScalar(dBType, ConnectionString);
            Assert.AreEqual("first1", name);
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void ExecuteReaderTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            string i = Utils.ExecuteReader(dBType, ConnectionString);
            Assert.AreEqual("FIRST1", i.ToUpper());
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void DataAdapterTest(ASqlManager.DBType dBType, string ConnectionString)
        {
            int i = Utils.DataAdapter(dBType, ConnectionString);
            Assert.AreEqual(1, i);
        }
        
        [DataRow(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString)]
        [DataRow(ASqlManager.DBType.Oracle, Utils.oraConnectionString)]
        [DataRow(ASqlManager.DBType.MySql, Utils.mysConnectionString)]
        [DataRow(ASqlManager.DBType.PostgreSQL, Utils.posConnectionString)]
        [DataRow(ASqlManager.DBType.Sqlite, Utils.litConnectionString)]
        [TestMethod]
        public void AllDbBasicFunctionalitiesTest(ASqlManager.DBType dBType, string ConnectionString) 
        {
            string check = Utils.CheckTable(dBType, ConnectionString);
            if ("PERSON" == check.ToUpper()) 
            {
                Utils.DropTable(dBType, ConnectionString);
            }
            int i = 1;
            i = Utils.CreateTable(dBType, ConnectionString);
            int count = 0;
            count = Utils.InsertRow(dBType, ConnectionString);
            count = Utils.InsertRowTransaction(dBType, ConnectionString);
            string name = "";
            name = Utils.ExecuteScalar(dBType, ConnectionString);
            name = Utils.ExecuteReader(dBType, ConnectionString);
            count = Utils.DataAdapter(dBType, ConnectionString);
        }
        
    }
}