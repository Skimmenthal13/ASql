using System.Data.Common;
using System.Data.SqlClient;

namespace ASql.Tester
{
    [TestClass]
    public class BasicTest
    {
        
        [DataRow("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=DBWUSR;Password=DBWUSR;", "SELECT 3+4 as nvalue FROM DUAL")]
        [TestMethod]
        public void SimpleQueryTest(string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.Oracle;


            using (ASqlConnection conn = new ASqlConnection(ConnectionString))
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        i = read.GetInt32(read.GetOrdinal("nvalue"));  
                    }
                }
                Assert.AreEqual(7, i);
            }
        }
    }
}