using Commons;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.AccountEntity;
using VideoHub.CommonEntity;

namespace VideoHub.Db
{
    public class UserDapperResponstory : IUserResponstory
    {
        private readonly IDbHelper _db;
        public UserDapperResponstory(IDbHelper db)
        {
            _db = db;
        }

        public int DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteUserByLoginName(string loginName)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUserByLoginNameAsync(string loginName)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLoginName(string loginName)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByLoginNameAsync(string LoginName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            using (var conn = _db.CreateDapperConnection())
            {
                return  conn.Query<User>($"select * from tuser");
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var conn = _db.CreateDapperConnection())
            {
                return await conn.QueryAsync<User>($"select * from tuser");
            }
        }

        public IEnumerable<User> GetUsersByPage(int pageIndex, int pageSize, ref int totalCount)
        {
            throw new NotImplementedException();
        }

        public Task<PageEntity<IEnumerable<User>>> GetUsersByPageAsync(PageEntityBase page)
        {
            throw new NotImplementedException();
        }

        public int InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
