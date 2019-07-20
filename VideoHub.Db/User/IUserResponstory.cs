using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace VideoHub.Db
{
    public interface IUserResponstory: IResponstory
    {
        Task<IEnumerable<User>> GetUsersAsync();
        IEnumerable<User> GetUsers();
        Task<PageEntity<IEnumerable<User>>> GetUsersByPageAsync(PageEntityBase page);
        IEnumerable<User> GetUsersByPage(int pageIndex, int pageSize, ref int totalCount);
        Task<User> GetUserByIdAsync(int id);
        User GetUserById(int id);
        Task<User> GetUserByLoginNameAsync(string LoginName);
        User GetUserByLoginName(string loginName);
        int InsertUser(User user);
        Task<int> InsertUserAsync(User user);
        int UpdateUser(User user);
        Task<int> UpdateUserAsync(User user);
        int DeleteUserByLoginName(string loginName);
        Task<int> DeleteUserByLoginNameAsync(string loginName);
        int DeleteUserById(int id);
        Task<int> DeleteUserByIdAsync(int id);
    }
}
