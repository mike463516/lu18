using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commons.DbHelper
{
    /// <summary>
    /// mysql的访问帮助类
    /// </summary>
    public class MySqlHelper : IDbHelper
    {
        /// <summary>
        /// 默认链接字符串
        /// </summary>
        private static readonly string DefaultConnectionString = @"server=localhost;Database=videohub;Uid=root;Pwd=abc-123;";
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public MySqlHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateDapperConnection(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _configuration.GetConnectionString("MySql");
            }
            IDbConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public ISqlSugarClient CreateSqlSugarConnection(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _configuration.GetConnectionString("MySql");
            }
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = SqlSugar.DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            db.Aop.OnLogExecuting = (sql, par) => { }; ;//Enable log events
            db.Aop.OnLogExecuted = (sql, par) => { };
            return db;
        }
    }
}
