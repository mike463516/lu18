using Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace VideoHub.Db
{
    public class TagSqlSugarResponstory : ITagResponstory
    {
        private readonly IDbHelper _dbHelper;
        public TagSqlSugarResponstory(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<int> DeleteTagsByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new Exception("id不能为0");
            }
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Deleteable<Tag>(_ => _.Id == id).ExecuteCommandAsync();
            }
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Queryable<Tag>().ToListAsync();
            }
        }

        public async Task<IEnumerable<Tag>> GetAllTags(Expression<Func<Tag, bool>> ex)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Queryable<Tag>().Where(ex).ToListAsync();
            }
        }

        public async Task<Tag> GetTag(int id)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<Tag>().Where(_ => _.Id == id).ToListAsync())?.FirstOrDefault();
            }
        }

        public async Task<Tag> GetTag(string name)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return (await conn.Queryable<Tag>().Where(_ => _.Name.Equals(name)).ToListAsync())?.FirstOrDefault();
            }
        }

        public IEnumerable<Tag> GetTagsByPage(int pageIndex, int pageSize, ref int totalCount)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Queryable<Tag>().ToPageList(pageIndex, pageSize, ref totalCount);
            }
        }

        public async Task<PageEntity<IEnumerable<Tag>>> GetTagsByPageAsync(PageEntityBase page)
        {
            var result = new PageEntity<IEnumerable<Tag>>() { PageIndex = page.PageIndex, PageSize = page.PageSize };
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                int total = 0;
                result.Result = await Task.Run<IEnumerable<Tag>>(() => { return GetTagsByPage(page.PageIndex, page.PageSize, ref total); });
                result.Total = total;
                return result;
            }
        }

        public async Task<int> InsertTagsAsync(Tag tag)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Insertable<Tag>(tag).ExecuteCommandAsync();
            }
        }

        public async Task<int> InsertTagsAsync(IEnumerable<Tag> tags)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Insertable<List<Tag>>(tags.ToList()).ExecuteCommandAsync();
            }
        }

        public int UpdateTags(Tag tag)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return conn.Updateable<Tag>(tag).ExecuteCommand();
            }
        }

        public async Task<int> UpdateTagsAsync(Tag tag)
        {
            using (var conn = _dbHelper.CreateSqlSugarConnection())
            {
                return await conn.Updateable<Tag>(tag).ExecuteCommandAsync();
            }
        }
    }
}
