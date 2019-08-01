using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace VideoHub.Db
{
    public interface ITagResponstory : IResponstory
    {
        Task<IEnumerable<Tag>> GetAllTags();
        Task<IEnumerable<Tag>> GetAllTags(Expression<Func<Tag, bool>> ex);
        Task<Tag> GetTag(int id);
        Task<Tag> GetTag(string name);
        Task<PageEntity<IEnumerable<Tag>>> GetTagsByPageAsync(PageEntityBase page);
        IEnumerable<Tag> GetTagsByPage(int pageIndex, int pageSize, ref int totalCount);
        Task<int> InsertTagsAsync(Tag tag);
        Task<int> InsertTagsAsync(IEnumerable<Tag> tags);
        int UpdateTags(Tag tag);
        Task<int> UpdateTagsAsync(Tag tag);
        Task<int> DeleteTagsByIdAsync(int id);
    }
}
