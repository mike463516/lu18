using Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHub.AccountEntity;
using VideoHub.CommonEntity;

namespace VideoHub.Db
{
    public class UserSqlSugarResponstory : IUserResponstory
    {
        private readonly IDbHelper _dbHelper;
        public UserSqlSugarResponstory(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public int DeleteUserById(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Deleteable<User>(_ => _.Id == id).ExecuteCommand();
            }
        }
        public async Task<int> DeleteUserByIdAsync(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Deleteable<User>().Where(_ => _.Id == id).ExecuteCommandAsync();
            }
        }
        public int DeleteUserByLoginName(string loginName)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Deleteable<User>(_ => _.LoginName.Equals(loginName)).ExecuteCommand();
            }
        }

        public async Task<int> DeleteUserByLoginNameAsync(string loginName)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Deleteable<User>().Where(_ => _.LoginName.Equals(loginName)).ExecuteCommandAsync();
            }
        }

        public User GetUserById(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<User>().InSingle(id);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<User>().Where(_=>_.Id==id).ToListAsync())?.FirstOrDefault();
            }
        }

        public User GetUserByLoginName(string loginName)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<User>().Where(_ => _.LoginName.Equals(loginName)).ToList()?.FirstOrDefault();
            }
        }

        public async Task<User> GetUserByLoginNameAsync(string LoginName)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<User>().Where(_ => _.LoginName.Equals(LoginName)).ToListAsync())?.FirstOrDefault();
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<User>().ToList();
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Queryable<User>().ToListAsync();
            }
        }

        public IEnumerable<User> GetUsersByPage(int pageIndex, int pageSize, ref int totalCount)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<User>().ToPageList(pageIndex, pageSize, ref totalCount);
            }
        }
        public async Task<PageEntity<IEnumerable<User>>> GetUsersByPageAsync(PageEntityBase page)
        {
            var result = new PageEntity<IEnumerable<User>>() {  PageIndex = page.PageIndex,PageSize = page.PageSize};
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                int total = 0;
                result.Result =  await Task.Run<IEnumerable<User>>(() => { return GetUsersByPage(page.PageIndex, page.PageSize, ref total); });
                result.Total = total;
                return result;
            }
        }

        public int InsertUser(User user)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Insertable<User>(user).ExecuteCommand();
            }
        }
        public async Task<int> InsertUserAsync(User user)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Insertable<User>(user).ExecuteCommandAsync();
            }
        }
        public int UpdateUser(User user)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Updateable<User>(user).ExecuteCommand();
            }
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Updateable<User>(user).ExecuteCommandAsync();
            }
        }
    }
}
