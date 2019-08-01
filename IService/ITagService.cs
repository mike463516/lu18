using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Entities;

namespace IService
{
    public interface ITagService
    {
        Task<ResultEntity<PageEntity<IEnumerable<Tag>>>> GetTagsByPageAsync(PageEntityBase page);
        Task<ResultEntity<IEnumerable<Tag>>> GetTags();
        Task<ResultEntity<Tag>> GetTag(int id);
        Task<ResultEntity<Tag>> GetTag(string name);
        Task<ResultEntityBase> AddTag(Tag tag);
        Task<ResultEntityBase> UpdateTag(Tag tag);
    }
}
