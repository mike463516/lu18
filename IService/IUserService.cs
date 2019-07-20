using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace IService
{
    public interface IUserService
    {
        Task<ResultEntity<PageEntity<IEnumerable<User>>>> GetUsersByPageAsync(PageEntityBase page);
        Task<ResultEntity<User>> GetUserByLoginNameWithTypeAsync(string loginName,string password,int type);
        Task<ResultEntity<User>> GetUserByIdAsync(int id);
    }
}
