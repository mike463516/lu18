using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHub.AccountEntity;

namespace VideoHub.ServiceApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iuserService;
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