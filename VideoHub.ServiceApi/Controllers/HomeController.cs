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
    [Route ("api/[controller]")]
    [Produces ("application/json")]
    [ApiController]
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : ControllerBase 
    { 
        public HomeController(){

        }
        public async Task<string> Login(){
            return string.Empty;
        }
    }
}