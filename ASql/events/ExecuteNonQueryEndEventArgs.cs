using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASql.events
{
    public class ExecuteNonQueryEndEventArgs : EventArgs
    {
        /// <summary>
        /// Query.
        /// </summary>
        public string Query { get; set; } = null;

        /// <summary>
        /// Total runtime in milliseconds.
        /// </summary>
        public double TotalMilliseconds { get; set; } = 0;
    }
}
