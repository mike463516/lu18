using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public interface IDbHelper
    {
        IDbConnection CreateDapperConnection(string connectionString=null);
        ISqlSugarClient CreateSqlSugarConnection(string connectionString = null);
    }
}
