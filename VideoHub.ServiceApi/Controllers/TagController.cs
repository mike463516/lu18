using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace VideoHub.ServiceApi.Controllers
{
    /// <summary>
    /// 视频控制器
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    //[Authorize]
    public class TagController : ControllerBase
    {
        private readonly ITagService _itagService;
        private readonly ILogger<TagController> _ilogger;
        private readonly IConfiguration _iconfiguration;
        /// <summary>
        /// 视频控制器构造函数
        /// </summary>
        public TagController(ILogger<TagController> ilogger, ITagService itagService, IConfiguration iconfiguration)
        {
            _ilogger = ilogger;
            _itagService = itagService;
            _iconfiguration = iconfiguration;
        }
        /// <summary>
        /// 获取标签列表信息
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        [HttpGet, Route("List")]
        public async Task<ResultEntity<PageEntity<IEnumerable<Tag>>>> List([FromQuery]PageEntityBase pageEntity)
        {
            return await _itagService.GetTagsByPageAsync(pageEntity);
        }
        /// <summary>
        /// 通过id获取视频信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Get/{id:int}")]
        public async Task<ResultEntity<Tag>> Get(int id)
        {
            return await _itagService.GetTag(id);
        }
        /// <summary>
        /// 通过name获取视频信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet, Route("Get/{name}")]
        public async Task<ResultEntity<Tag>> Get(string name)
        {
            return await _itagService.GetTag(name);
        }
        /// <summary>
        /// 通过name获取视频信息
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost, Route("Add")]
        public async Task<ResultEntityBase> Add([FromBody]Tag tag)
        {
            return await _itagService.AddTag(tag);
        }
    }
}
