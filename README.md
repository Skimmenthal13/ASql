# ASql

C# Library that let use multiple types of databases with same code

The supported databases are SqlServer and Oracle

You only have to declare wich type of database would like you use and use the classe in the same way of sql or oracle client.

Here you can find examples : 

            # Example 1 Basic query

            public void ExecuteQuery(ASqlManager.DBType dBType, string ConnectionString, string sql)
            {
                ASqlManager.DataBaseType = dBType;

                using (ASqlConnection conn = new ASqlConnection(ConnectionString))
                {
                    int i = 0;
                    conn.Open();
                    ASqlCommand cmd = new ASqlCommand(sql, conn);
                    i = cmd.ExecuteNonQuery();
                }
            }

            # Example 2 Query With Parameters

            public void ExecuteScalar(ASqlManager.DBType dBType, string ConnectionString)
            {
                string sql = "select firstname from person where lastname = @lastname";
                //if you want to use oracle you have tu put : intestad @ like this 
                //string sql = "select firstname from person where lastname = :lastname";
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
                    //Pay attention to popolate the aSqlParameters collection like the row below and not the Parameters collection (the library will think to keep uptodated the Parameters collection)
                    cmd.aSqlParameters.Add(par);
                    name = (string)cmd.ExecuteScalar();
                }
            }

            # Example 3 Using DataAdapter

            public void SqlDataAdapterTester(ASqlManager.DBType dBType)
            {
                ASqlManager.DataBaseType = dBType;
                string sql = "select * from person"
                using (ASqlConnection conn = new ASqlConnection(ConnectionString))
                {
                    int i = 0;
                    conn.Open();
                    ASqlCommand cmd = new ASqlCommand(sql, conn);
                    ASqlDataAdapter aSqlDataAdapter = new ASqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    i = aSqlDataAdapter.Fill(ds);

                }
            }

            # Example 4 using Transaction

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
                }
            }
