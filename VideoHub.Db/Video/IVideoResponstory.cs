using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;
using VideoHub.Entities.mapping;

namespace VideoHub.Db
{
    public interface IVideoResponstory : IResponstory
    {
        Task<Video> GetVideoByUidAsync(string uid);
        Task<Video> GetVideoByIdAsync(int id);
        Task<IEnumerable<Video>> GetVideosAsync();
        Task<PageEntity<IEnumerable<Video>>> GetVideosByPageAsync(PageEntityBase page);
        IEnumerable<Video> GetVideosByPage(int pageIndex, int pageSize, ref int totalCount);
        Task<int> InsertVideoAsync(Video video);
        Task<int> InsertVideosAsync(IEnumerable<Video> videos);
        int UpdateVideo(Video video);
        Task<int> UpdateVideoAsync(Video video);
        Task<int> UpdateVideoAsync(VideoMapper videoMapper);
        Task<int> DeleteVideoByIdAsync(int id);
        Task<int> DeleteVideoByUidAsync(string uid);
        Task<int> InsertVideoAsync(VideoMapper videoMapper);
        Task<int> InsertVideosAsync(IEnumerable<VideoMapper> videoMappers);
        Task<VideoMapper> GetVideoMapperByUidAsync(string uid);
        Task<VideoMapper> GetVideoMapperByIdAsync(int id);
    }
}
