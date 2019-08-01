using Commons;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;
using VideoHub.Entities.mapping;

namespace VideoHub.Db
{
    public class VideoSqlSugarResponstory : IVideoResponstory
    {
        private readonly IDbHelper _dbHelper;
        public VideoSqlSugarResponstory(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<int> DeleteVideoByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new Exception("id不能为0");
            }
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                try
                {
                    conn.Ado.BeginTran();
                    await conn.Deleteable<Video>(_ => _.Id == id).ExecuteCommandAsync();
                    await conn.Deleteable<VideoTag>().Where(_ => _.VideoId == id).ExecuteCommandAsync();
                    await conn.Deleteable<VideoCategory>().Where(_ => _.VideoId == id).ExecuteCommandAsync();
                    conn.Ado.CommitTran();
                    return 1;
                }
                catch (Exception)
                {
                    conn.Ado.RollbackTran();
                    return 0;
                }
            }
        }

        public async Task<int> DeleteVideoByUidAsync(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                throw new Exception("参数错误");
            }
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Deleteable<Video>(_ => uid.Equals(_.Uid)).ExecuteCommandAsync();
            }
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<Video>().Where(_ => _.Id == id).ToListAsync())?.FirstOrDefault();
            }
        }

        public async Task<Video> GetVideoByUidAsync(string uid)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<Video>().Where(_ => _.Uid.Equals(uid)).ToListAsync())?.FirstOrDefault();
            }
        }

        public async Task<VideoMapper> GetVideoMapperByIdAsync(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<VideoMapper>().Where((v) => v.Id == id).Mapper((v, cache) =>
                {
                    var tagids = cache.Get<List<int>>(list =>
                    {
                        return conn.Queryable<VideoTag>().Where(_ => _.VideoId == v.Id).Select(_ => _.TagId).ToList();
                    });
                    var categoryids = cache.Get<List<int>>(list =>
                    {
                        return conn.Queryable<VideoCategory>().Where(_ => _.VideoId == v.Id).Select(_ => _.CategoryId).ToList();
                    });
                    v.Tags = conn.Queryable<Tag>().Where(_ => tagids.Contains(_.Id)).ToList();
                    v.Categories = conn.Queryable<Category>().Where(_ => categoryids.Contains(_.Id)).ToList();
                }).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task<VideoMapper> GetVideoMapperByUidAsync(string uid)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<VideoMapper>().Where((v) => v.Uid == uid).Mapper(async (v, cache) =>
                {
                    var tagids = cache.Get<List<int>>(list =>
                    {
                        return conn.Queryable<VideoTag>().Where(_ => _.VideoId == v.Id).Select(_ => _.TagId).ToList();
                    });
                    var categoryids = cache.Get<List<int>>(list =>
                   {
                       return conn.Queryable<VideoCategory>().Where(_ => _.VideoId == v.Id).Select(_ => _.CategoryId).ToList();
                   });
                    v.Tags = await conn.Queryable<Tag>().Where(_ => tagids.Contains(_.Id)).ToListAsync();
                    v.Categories = await conn.Queryable<Category>().Where(_ => categoryids.Contains(_.Id)).ToListAsync();
                }).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Video>> GetVideosAsync()
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Queryable<Video>().ToListAsync();
            }
        }

        public IEnumerable<Video> GetVideosByPage(int pageIndex, int pageSize, ref int totalCount)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<Video>().OrderBy(_ => _.UpdateTime, OrderByType.Desc).ToPageList(pageIndex, pageSize, ref totalCount);
            }
        }

        public async Task<PageEntity<IEnumerable<Video>>> GetVideosByPageAsync(PageEntityBase page)
        {
            var result = new PageEntity<IEnumerable<Video>>() { PageIndex = page.PageIndex, PageSize = page.PageSize };
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                int total = 0;
                result.Result = await Task.Run<IEnumerable<Video>>(() => { return GetVideosByPage(page.PageIndex, page.PageSize, ref total); });
                result.Total = total;
                return result;
            }
        }

        public async Task<int> InsertVideoAsync(Video video)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Insertable<Video>(video).ExecuteCommandAsync();
            }
        }

        public async Task<int> InsertVideoAsync(VideoMapper videoMapper)
        {

            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                try
                {
                    conn.Ado.BeginTran();
                    var videoid = await conn.Insertable<Video>(videoMapper).ExecuteReturnIdentityAsync();
                    foreach (var item in videoMapper.Categories)
                    {
                        conn.AddQueue("insert into tvideocategory (VideoId,CategoryId) values (@VideoId,@CategoryId)", new { VideoId = videoid, CategoryId = item.Id });
                    }
                    foreach (var item in videoMapper.Tags)
                    {
                        conn.AddQueue("insert into tvideotag (VideoId,TagId) values (@VideoId,@TagId)", new { VideoId = videoid, TagId = item.Id });
                    }
                    await conn.SaveQueuesAsync();
                    conn.Ado.CommitTran();
                    return 1;
                }
                catch (Exception)
                {
                    conn.Ado.RollbackTran();
                    return 0;
                }
            }

        }

        public async Task<int> InsertVideosAsync(IEnumerable<Video> videos)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Insertable<List<Video>>(videos.ToList()).ExecuteCommandAsync();
            }
        }

        public async Task<int> InsertVideosAsync(IEnumerable<VideoMapper> videoMappers)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                try
                {
                    conn.Ado.BeginTran();
                    foreach (var videoMapper in videoMappers)
                    {
                        var videoid = await conn.Insertable<Video>(videoMapper).ExecuteReturnIdentityAsync();
                        foreach (var item in videoMapper.Categories)
                        {
                            conn.AddQueue("insert into tvideocategory (VideoId,CategoryId) values (@VideoId,@CategoryId)", new { VideoId = videoid, CategoryId = item.Id });
                        }
                        foreach (var item in videoMapper.Tags)
                        {
                            conn.AddQueue("insert into tvideotag (VideoId,TagId) values (@VideoId,@TagId)", new { VideoId = videoid, TagId = item.Id });
                        }
                        await conn.SaveQueuesAsync();
                    }
                    conn.Ado.CommitTran();
                    return 1;
                }
                catch (Exception)
                {
                    conn.Ado.RollbackTran();
                    return 0;
                }
            }
        }

        public int UpdateVideo(Video video)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Updateable<Video>(video).ExecuteCommand();
            }
        }

        public async Task<int> UpdateVideoAsync(Video video)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Updateable<Video>(video).ExecuteCommandAsync();
            }
        }

        public async Task<int> UpdateVideoAsync(VideoMapper videoMapper)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                try
                {
                    conn.Ado.BeginTran();
                    await conn.Updateable<Video>(videoMapper).ExecuteCommandAsync();
                    await conn.Deleteable<VideoTag>().Where(_ => _.VideoId == videoMapper.Id).ExecuteCommandAsync();
                    await conn.Deleteable<VideoCategory>().Where(_ => _.VideoId == videoMapper.Id).ExecuteCommandAsync();
                    foreach (var item in videoMapper.Categories)
                    {
                        conn.AddQueue("insert into tvideocategory (VideoId,CategoryId) values (@VideoId,@CategoryId)", new { VideoId = videoMapper.Id, CategoryId = item.Id });
                    }
                    foreach (var item in videoMapper.Tags)
                    {
                        conn.AddQueue("insert into tvideotag (VideoId,TagId) values (@VideoId,@TagId)", new { VideoId = videoMapper.Id, TagId = item.Id });
                    }
                    await conn.SaveQueuesAsync();
                    conn.Ado.CommitTran();
                    return 1;
                }
                catch (Exception)
                {
                    conn.Ado.RollbackTran();
                    return 0;
                }
            }
        }
    }
}
