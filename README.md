# ASql

C# Library that let use multiple types of databases with same code

The supported databases are SqlServer and Oracle

You only have to declare wich type of database would like you use and use the classe in the same way of sql or oracle client.

Here you can find examples : 

            Example 1 Basic query

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
            }

            Example 2 Query With Parameters

            public void InsertRow(ASqlManager.DBType dBType, string ConnectionString, string sql) 
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
                ASqlCommand cmd = new ASqlCommand(sql, conn);
                List<ASqlParameter> apc = GetParametersFromkeyValuePairs(d);
                foreach (ASqlParameter param in apc)
                {
                    cmd.aSqlParameters.Add(param);
                }
                
                i = cmd.ExecuteNonQuery();
                Assert.AreEqual(1, i);
            }
        }

        internal List<ASqlParameter> GetParametersFromkeyValuePairs(Dictionary<string, object> keyValuePairs)
        {
            string vals = "";
            List<ASqlParameter> prm = new List<ASqlParameter>();
            foreach (KeyValuePair<string, object> currKvp in keyValuePairs)
            {

                if (currKvp.Value is DateTime
                    || currKvp.Value is DateTime?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is DateTimeOffset
                    || currKvp.Value is DateTimeOffset?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is int
                    || currKvp.Value is long
                    || currKvp.Value is decimal)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is bool)
                {
                    string val = ((bool)currKvp.Value ? "1" : "0");
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = val;
                    prm.Add(para);
                }
                else if (currKvp.Value is byte[])
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is string)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is Guid)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value.ToString();
                    prm.Add(para);
                }
                else
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = ":" + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }


            }
            return prm;
        }
