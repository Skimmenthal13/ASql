﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASql.Tester
{
    class Utils
    {
        static internal List<ASqlParameter> GetParametersFromkeyValuePairs(Dictionary<string, object> keyValuePairs, string paramChar)
        {
            string vals = "";
            List<ASqlParameter> prm = new List<ASqlParameter>();
            foreach (KeyValuePair<string, object> currKvp in keyValuePairs)
            {

                if (currKvp.Value is DateTime
                    || currKvp.Value is DateTime?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is DateTimeOffset
                    || currKvp.Value is DateTimeOffset?)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is int
                    || currKvp.Value is long
                    || currKvp.Value is decimal)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is bool)
                {
                    int val = ((bool)currKvp.Value ? 1 : 0); 
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = val;
                    prm.Add(para);
                }
                else if (currKvp.Value is byte[])
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is string)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }
                else if (currKvp.Value is Guid)
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value.ToString();
                    prm.Add(para);
                }
                else
                {
                    ASqlParameter para = new ASqlParameter();
                    para.ParameterName = paramChar + currKvp.Key;
                    para.Direction = ParameterDirection.Input;
                    para.Value = currKvp.Value;
                    prm.Add(para);
                }


            }
            return prm;
        }

    }
}