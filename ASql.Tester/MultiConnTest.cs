using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASql.Tester
{
    [TestClass]
    public class MultiConnTest
    {
        [TestMethod]
        public void ExecuteScalarMultiConnTest()
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.MultiDatabase;
            ASqlConnection sqlConn = new ASqlConnection(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString);
            ASqlConnection oraConn = new ASqlConnection(ASqlManager.DBType.Oracle, Utils.oraConnectionString);
            sqlConn.Open();
            oraConn.Open();
            string sqlName = Utils.ExecuteScalar(sqlConn, Utils.sqlSelectFirstName);
            string oraName = Utils.ExecuteScalar(oraConn, Utils.oraSelectFirstName);
            sqlConn.Close();
            oraConn.Close();
        }
        [TestMethod]
        public void ExecuteReaderMultiConnTest()
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.MultiDatabase;
            ASqlConnection sqlConn = new ASqlConnection(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString);
            ASqlConnection oraConn = new ASqlConnection(ASqlManager.DBType.Oracle, Utils.oraConnectionString);
            sqlConn.Open();
            oraConn.Open();
            string sqlName = Utils.ExecuteReader(sqlConn, Utils.sqlSelectFirstName);
            string oraName = Utils.ExecuteReader(oraConn, Utils.oraSelectFirstName);
            sqlConn.Close();
            oraConn.Close();
        }
        [TestMethod]
        public void InsertRowTransactionWithMultiConnTest()
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.MultiDatabase;
            ASqlConnection sqlConn = new ASqlConnection(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString);
            ASqlConnection oraConn = new ASqlConnection(ASqlManager.DBType.Oracle, Utils.oraConnectionString);
            sqlConn.Open();
            oraConn.Open();
            DbTransaction sqlTrans = sqlConn.BeginTransaction();
            DbTransaction oraTrans = oraConn.BeginTransaction();
            Utils.InsertRow(sqlConn, Utils.sqlInsertRow, sqlTrans);
            Utils.InsertRow(oraConn, Utils.oraInsertRow, oraTrans);
            sqlTrans.Rollback();
            oraTrans.Rollback();
            sqlConn.Close();
            oraConn.Close();
        }
        [TestMethod]
        public void DataAdapterWithMultiConnTest()
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.MultiDatabase;
            ASqlConnection sqlConn = new ASqlConnection(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString);
            ASqlConnection oraConn = new ASqlConnection(ASqlManager.DBType.Oracle, Utils.oraConnectionString);
            sqlConn.Open();
            oraConn.Open();
            int sqlCount = Utils.DataAdapter(sqlConn, Utils.sqlSelectStar);
            int oraCount = Utils.DataAdapter(oraConn, Utils.oraSelectStar);
            sqlConn.Close();
            oraConn.Close();
        }
        [TestMethod]
        public void AllDbBasicFunctionalitiesTest()
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.MultiDatabase;
            ASqlConnection sqlConn = new ASqlConnection(ASqlManager.DBType.SqlServer, Utils.sqlConnectionString);
            ASqlConnection oraConn = new ASqlConnection(ASqlManager.DBType.Oracle, Utils.oraConnectionString);
            sqlConn.Open();
            oraConn.Open();
            string sqlCheck = Utils.CheckTable(sqlConn,Utils.sqlCheckTable);
            string oraCheck = Utils.CheckTable(oraConn, Utils.oraCheckTable);
            if ("PERSON" == sqlCheck.ToUpper())
            {
                Utils.DropTable(sqlConn, Utils.sqlDropTable);
            }
            if ("PERSON" == oraCheck.ToUpper())
            {
                Utils.DropTable(oraConn, Utils.oraDropTable);
            }
            int sqli = 1;
            sqli = Utils.CreateTable(sqlConn, Utils.sqlCreateTable);
            int orai = 1;
            orai = Utils.CreateTable(oraConn, Utils.oraCreateTable);
            int sqlcount = 0;
            sqlcount = Utils.InsertRow(sqlConn, Utils.sqlInsertRow);
            DbTransaction sqlTrans = sqlConn.BeginTransaction();
            sqlcount = Utils.InsertRow(sqlConn, Utils.sqlInsertRow, sqlTrans);
            int oracount = 0;
            oracount = Utils.InsertRow(oraConn, Utils.oraInsertRow);
            DbTransaction oraTrans = oraConn.BeginTransaction();
            oracount = Utils.InsertRow(oraConn, Utils.oraInsertRow, oraTrans);
            sqlTrans.Rollback();
            oraTrans.Rollback();
            string sqlname = "";
            sqlname = Utils.ExecuteScalar(sqlConn, Utils.sqlSelectFirstName);
            sqlname = Utils.ExecuteReader(sqlConn, Utils.sqlSelectFirstName);
            sqlcount = Utils.DataAdapter(sqlConn, Utils.sqlSelectStar);
            string oraname = "";
            oraname = Utils.ExecuteScalar(oraConn, Utils.oraSelectFirstName);
            oraname = Utils.ExecuteReader(oraConn, Utils.oraSelectFirstName);
            oracount = Utils.DataAdapter(oraConn, Utils.oraSelectStar);
            sqlConn.Close();
            oraConn.Close();
        }
    }
}
