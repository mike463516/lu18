using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.Entities;

namespace VideoHub.ServiceCollectionEx.IService
{
    public interface IUserService
    {
        [Get("/api/Account/GetUsers")]
        Task<IEnumerable<User>> GetUsers();
        [Get("/api/Account/GetUser")]
        Task<User> GetUserByLoginNameWithType(string loginName,string password,int type);
        [Get("/api/Account/GetUserById/{id}")]
        Task<User> GetUserById(int id);
    }
}
