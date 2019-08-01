using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Db;
using VideoHub.Entities;
using VideoHub.Entities.mapping;

namespace Service
{
    public class VideoService : IVideoService
    {
        private readonly IVideoResponstory _videoResponstory;
        public VideoService(IVideoResponstory videoResponstory)
        {
            _videoResponstory = videoResponstory;
        }
        public async Task<ResultEntity<PageEntity<IEnumerable<Video>>>> GetVideosByPageAsync(PageEntityBase page)
        {
            var result = new ResultEntity<PageEntity<IEnumerable<Video>>>() { };
            var videos = await _videoResponstory.GetVideosByPageAsync(page);
            if (videos.Result == null || videos.Result.Count()==0)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在视频";
                return result;
            }
            result.Result = videos;
            return result;
        }

        public async Task<ResultEntity<Video>> GetVideoByIdAsync(int id)
        {
            var result = new ResultEntity<Video>() { };
            var video = await _videoResponstory.GetVideoByIdAsync(id);
            if (video == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在该视频";
                return result;
            }
            result.Result = video;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntity<Video>> GetVideoByUidAsync(string uid)
        {
            var result = new ResultEntity<Video>() { };
            var video = await _videoResponstory.GetVideoByUidAsync(uid);
            if (video == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在该视频";
                return result;
            }
            result.Result = video;
            return await Task.FromResult(result);
        }
        public async Task<ResultEntityBase> InsertVideoAsync(Video video)
        {
            var result = new ResultEntityBase() { };
            var count = await _videoResponstory.InsertVideoAsync(video);
            if (count>0)
            {
                return result;
            }
            result.Status = StatusCode.Error;
            result.Message = "视频信息插入失败";
            return result;
        }

        public async Task<ResultEntity<VideoMapper>> GetVideoMapperByUidAsync(string uid)
        {
            var result = new ResultEntity<VideoMapper>() { };
            var video = await _videoResponstory.GetVideoMapperByUidAsync(uid);
            if (video == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在该视频";
                return result;
            }
            result.Result = video;
            return await Task.FromResult(result);
        }
    }
}
