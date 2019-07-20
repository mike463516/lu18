using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHub.Entities;

namespace VideoHub.ServiceApi.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iuserService;
        /// <summary>
        /// 用户控制器构造函数
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _iuserService = userService;
        }

        [HttpGet,Route("GetUserById")]
        public async Task<User> GetUserById(int id)
        {
            return (await _iuserService.GetUserByIdAsync(id)).Result;
        }
        [HttpGet, Route("GetUserByLoginNameWithType")]
        public async Task<User> GetUserByLoginNameWithType(string loginName, string password, int type)
        {
            await Task.CompletedTask;
            return null;
        }
        [HttpGet, Route("GetUsers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            await Task.CompletedTask;
            return null;
        }
    }
}