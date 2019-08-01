using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;
using VideoHub.Entities.mapping;
using VideoHub.ServiceApi.Commons;

namespace VideoHub.ServiceApi.Controllers
{
    /// <summary>
    /// 视频控制器
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    [ApiController]
    //[Authorize]
    public class VideoController : ControllerBase
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly IVideoService _ivideoService;
        private readonly ILogger<VideoController> _ilogger;
        private readonly IConfiguration _iconfiguration;
        /// <summary>
        /// 视频控制器构造函数
        /// </summary>
        public VideoController(ILogger<VideoController> ilogger, IVideoService ivideoService, IConfiguration iconfiguration)
        {
            _ilogger = ilogger;
            _ivideoService = ivideoService;
            _iconfiguration = iconfiguration;
        }
        /// <summary>
        /// 获取视频列表信息
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <returns></returns>
        [HttpGet, Route("List")]
        public async Task<ResultEntity<PageEntity<IEnumerable<Video>>>> List([FromQuery]PageEntityBase pageEntity)
        {
            return await _ivideoService.GetVideosByPageAsync(pageEntity);
        }
        ///// <summary>
        ///// 通过uid获取视频信息
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //[HttpGet, Route("Get/{uid}")]
        //public async Task<ResultEntity<Video>> Get(string uid)
        //{
        //    return await _ivideoService.GetVideoByUidAsync(uid);
        //}
        /// <summary>
        /// 通过uid获取视频信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet, Route("Get/{uid}")]
        public async Task<ResultEntity<VideoMapper>> Get(string uid)
        {
            return await _ivideoService.GetVideoMapperByUidAsync(uid);
        }
        /// <summary>
        /// 上传视频文件
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("UploadVideo")]
        [DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ResultEntityBase> UploadVideo(CancellationToken cancellationToken)
        {
            var result = new ResultEntityBase();
            var video = new Video();
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                result.Status = CommonEntity.StatusCode.Error;
                result.Message = "请求数据类型出错";
                return await Task.FromResult(result);
            }
            var formAccumulator = new KeyValueAccumulator();

            string videoUid = Guid.NewGuid().ToString("N");
            video.Uid = videoUid;
            video.State = 1;
            string targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{_iconfiguration.GetValue<string>("VirtualPath")}/{videoUid}");
            //检查相应目录
            if (!Directory.Exists(targetFilePath))
            {
                Directory.CreateDirectory(targetFilePath);
            }
            var boundary = MultipartRequestHelper.GetBoundary(
              MediaTypeHeaderValue.Parse(Request.ContentType),
              _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            try
            {
                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    ContentDispositionHeaderValue contentDisposition;
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            var fileName = section.AsFileSection().FileName;
                            video.FileName = fileName;
                            video.Name = fileName;
                            video.UpdateUser = "Admin";
                            video.CreateUser = "Admin";
                            targetFilePath = Path.Combine(targetFilePath, fileName);
                            if (System.IO.File.Exists(targetFilePath))
                            {
                                result.Status = CommonEntity.StatusCode.Error;
                                result.Message = "文件已存在";
                                break;
                            }
                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);
                                _ilogger.LogInformation($"复制文件到该路径下 '{targetFilePath}'");
                            }
                        }
                        else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        {
                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = GetEncoding(section);
                            using (var streamReader = new StreamReader(
                                section.Body,
                                encoding,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                                if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = String.Empty;
                                }
                                formAccumulator.Append(key.ToString(), value);

                                if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                                {
                                    throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                                }
                            }
                        }
                    }
                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }
                video.Name = formAccumulator.GetResults()["name"];
                video.Title = formAccumulator.GetResults()["title"];
                video.SecertKey = videoUid;
                video.State = Convert.ToInt32(formAccumulator.GetResults()["state"]);
                return await Task.FromResult(await _ivideoService.InsertVideoAsync(video));
            }
            catch (OperationCanceledException)
            {
                if (System.IO.File.Exists(targetFilePath))
                {
                    System.IO.File.Delete(targetFilePath);
                }
                result.Status = CommonEntity.StatusCode.Error;
                result.Message = "用户取消上传操作";
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(targetFilePath))
                {
                    System.IO.File.Delete(targetFilePath);
                }
                result.Status = CommonEntity.StatusCode.Error;
                result.Message = "取消上传操作";
                _ilogger.LogError(new EventId(),ex,ex.Message);
                return await Task.FromResult(result);
            }
        }
        /// <summary>
        /// 下载视频
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("DownLoadVideo")]
        public async Task<Stream> DownLoadVideo()
        {
            await Task.CompletedTask;
            return null;
        }
        private Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}
