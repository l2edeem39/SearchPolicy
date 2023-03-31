using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPolicy.Share.Utility
{
    public class SqlParameterHelper
    {
        public static string SqlParameterNameMatch(SqlParameter[] parameter)
        {
            string parameterNameAppend = string.Empty;
            var parameterNames = parameter.Select(s => s.ParameterName);
            foreach (var paramName in parameterNames)
            {
                parameterNameAppend += string.IsNullOrEmpty(parameterNameAppend) ? " " + paramName : "," + paramName;
            }
            return parameterNameAppend;
        }
    }
}
