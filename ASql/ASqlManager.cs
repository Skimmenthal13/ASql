using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASql
{
    public class ASqlManager
    {
        public enum DBType
        {
            SqlServer,
            Oracle,
            MySql
        }
        public static DBType DataBaseType { get; set; }
        
    }
}
