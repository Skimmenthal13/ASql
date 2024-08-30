using System.Data.Common;
using System.Data.SqlClient;

namespace ASql.Tester
{
    [TestClass]
    public class BasicTest
    {
        [DataRow("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.2.58)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=METASTORM;Password=metastorm;", "SELECT 3+4 as numero FROM DUAL")]
        [TestMethod]
        public void SimpleQueryTest(string ConnectionString, string sql)
        {
            ASqlManager.DataBaseType = ASqlManager.DBType.Oracle;


            using (ASqlConnection conn = new ASqlConnection(ConnectionString))//TODO The connection isn't handled neither opened at beginning nor closed at the end 
            {
                int i = 0;
                conn.Open();
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                using (DbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        i = read.GetInt32(read.GetOrdinal("numero"));  
                    }
                }
                Assert.AreEqual(7, i);
                conn.Close();
            }
        }
    }
}