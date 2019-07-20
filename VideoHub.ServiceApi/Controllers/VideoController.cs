using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly ILogger<VideoController> _ilogger;
        /// <summary>
        /// 视频控制器构造函数
        /// </summary>
        public VideoController(ILogger<VideoController> ilogger)
        {
            _ilogger = ilogger;
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
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                result.Status = CommonEntity.StatusCode.Error;
                result.Message = "请求数据类型出错";
                return await Task.FromResult(result);
            }
            //    var formAccumulator = new KeyValueAccumulator();

            string videoUid = Guid.NewGuid().ToString("N");
            string targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"/wwwroot/files/{videoUid}");
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
                            targetFilePath = Path.Combine(targetFilePath, fileName);
                            if (System.IO.File.Exists(targetFilePath))
                            {
                                result.Status = CommonEntity.StatusCode.Error;
                                result.Message = "文件已存在";
                                break;
                            }
                            using (var targetStream = System.IO.File.Create(Path.Combine(targetFilePath, fileName)))
                            {
                                await section.Body.CopyToAsync(targetStream);
                                _ilogger.LogInformation($"复制文件到该路径下 '{Path.Combine(targetFilePath, fileName)}'");
                            }
                        }
                        //else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        //{
                        //    // Content-Disposition: form-data; name="key"
                        //    //
                        //    // value

                        //    // Do not limit the key name length here because the 
                        //    // multipart headers length limit is already in effect.
                        //    var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        //    var encoding = GetEncoding(section);
                        //    using (var streamReader = new StreamReader(
                        //        section.Body,
                        //        encoding,
                        //        detectEncodingFromByteOrderMarks: true,
                        //        bufferSize: 1024,
                        //        leaveOpen: true))
                        //    {
                        //        // The value length limit is enforced by MultipartBodyLengthLimit
                        //        var value = await streamReader.ReadToEndAsync();
                        //        if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                        //        {
                        //            value = String.Empty;
                        //        }
                        //        formAccumulator.Append(key.ToString(), value);

                        //        if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                        //        {
                        //            throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                        //        }
                        //    }
                        //}
                    }
                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }
                return await Task.FromResult(result);
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
    }
}
