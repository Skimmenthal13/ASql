# ASql

C# Library that let use multiple types of databases with same code

The supported databases are SqlServer and Oracle

You only have to declare wich type of database would like you use and use the classe in the same way of sql or oracle client.

Here you can find an example : 

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
