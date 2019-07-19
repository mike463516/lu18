using IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.AccountEntity;
using VideoHub.CommonEntity;
using VideoHub.Db;
namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUserResponstory _iuserResponstory;
        public UserService(IUserResponstory userResponstory)
        {
            _iuserResponstory = userResponstory;
        }
        public async Task<ResultEntity<User>> GetUserByIdAsync(int id)
        {
            var result = new ResultEntity<User>() {};
            var user = await _iuserResponstory.GetUserByIdAsync(id);
            if (user == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在该用户";
                return result;
            }
            result.Result = user;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntity<User>> GetUserByLoginNameWithTypeAsync(string loginName, string password, int type)
        {
            var result = new ResultEntity<User>() { };
            var user =  await _iuserResponstory.GetUserByLoginNameAsync(loginName);
            if (user == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在该用户";
                return result;
            }
            if (user.Type != type)
            {
                result.Status = StatusCode.Error;
                result.Message = $"身份认证失败";
                return result;
            }
            if (!user.Password.Equals(password))
            {
                result.Status = StatusCode.Error;
                result.Message = $"密码错误";
                return result;
            }
            result.Result = user;
            return result;
        }

        public async Task<ResultEntity<PageEntity<IEnumerable<User>>>> GetUsersByPageAsync(PageEntityBase page)
        {
            var result = new ResultEntity<PageEntity<IEnumerable<User>>>() { };
            var users = await _iuserResponstory.GetUsersByPageAsync(page);
            if (users.Result == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在用户";
                return result;
            }
            return result;
        }
    }
}
