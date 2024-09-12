using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASql.Utils
{
    internal class ReflectionHelper
    {
        internal static string GetMethodFullName(MethodBase method) 
        {
            string methodName = method.Name;
            string className = method.ReflectedType.Name;

            string fullMethodName = className + "." + methodName;
            return fullMethodName;
        }
    }
}
