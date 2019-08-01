using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;
using VideoHub.Entities.mapping;

namespace IService
{
    public interface IVideoService
    {
        Task<ResultEntity<PageEntity<IEnumerable<Video>>>> GetVideosByPageAsync(PageEntityBase page);
        Task<ResultEntity<Video>> GetVideoByIdAsync(int id);
        Task<ResultEntity<Video>> GetVideoByUidAsync(string uid);
        Task<ResultEntity<VideoMapper>> GetVideoMapperByUidAsync(string uid);
        Task<ResultEntityBase> InsertVideoAsync(Video video);
    }
}
