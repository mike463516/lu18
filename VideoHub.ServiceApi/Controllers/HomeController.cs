using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHub.CommonEntity;
using VideoHub.ServiceApi.Utils;
using VideoHub.ViewModelEntity;

namespace VideoHub.ServiceApi.Controllers 
{
    /// <summary>
    /// 主页控制器 
    /// </summary>
    [Route ("api/[controller]")]
    [Produces ("application/json")]
    [ApiController]
    public class HomeController : ControllerBase 
    {
        private readonly IUserService _iuserService;
        private readonly IJwtHelper _ijwtHelper;
        /// <summary>
        /// HomeController构造函数
        /// </summary>
        /// <param name="iuserService"></param>
        /// <param name="ijwtHelper"></param>
        public HomeController(IUserService iuserService, IJwtHelper ijwtHelper)
        {
            _iuserService = iuserService;
            _ijwtHelper = ijwtHelper;
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model">用户登录实体</param>
        /// <returns></returns>
        [HttpPost,Route("Login")]
        public async Task<ResultEntity<TokenEntity>> LoginAsync([FromBody]LoginModel model){
            var result = new ResultEntity<TokenEntity>();
            if (model!=null||string.IsNullOrEmpty(model.LoginName)||String.IsNullOrEmpty(model.Password))
            {
                result.Message = "参数错误";
                result.Status = CommonEntity.StatusCode.Success;
                return await Task.FromResult(result);
            }
            var userInfo = await _iuserService.GetUserByLoginNameWithTypeAsync(model.LoginName, model.Password, 0);
            if (userInfo.Status!= CommonEntity.StatusCode.Success)
            {
                result.Message = userInfo.Message;
                result.Status = userInfo.Status;
                return await Task.FromResult(result);
            }
            var token = _ijwtHelper.GetToken(userInfo.Result);
            result.Result = new TokenEntity
            {
                AccessToken = token
            };
            result.Status = CommonEntity.StatusCode.Success;
            return await Task.FromResult(result);;
        }
    }
}