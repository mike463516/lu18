using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoHub.CommonEntity;
using VideoHub.Db;
using VideoHub.Entities;

namespace Service
{
    public class TagService : ITagService
    {
        private readonly ITagResponstory _tagResponstory;
        public TagService(ITagResponstory tagResponstory)
        {
            _tagResponstory = tagResponstory;
        }

        public async Task<ResultEntityBase> AddTag(Tag tag)
        {
            var result = new ResultEntityBase { };
            if (await _tagResponstory.GetTag(tag.Name)!=null)
            {
                result.Status = StatusCode.Error;
                result.Message = "当前标签已存在";
                return await Task.FromResult(result);
            }
            if (await _tagResponstory.InsertTagsAsync(tag) != 0)
            {
                result.Status = StatusCode.Success;
                result.Message = "标签添加成功";
                return await Task.FromResult(result);
            }
            else
            {
                result.Status = StatusCode.Error;
                result.Message = "标签添加失败";
                return await Task.FromResult(result);
            }
        }

        public async Task<ResultEntity<Tag>> GetTag(int id)
        {
            var result = new ResultEntity<Tag>() { };
            var tags = await _tagResponstory.GetTag(id);
            if (tags == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在标签";
                return await Task.FromResult(result);
            }
            result.Result = tags;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntity<Tag>> GetTag(string name)
        {
            var result = new ResultEntity<Tag>() { };
            var tags = await _tagResponstory.GetTag(name);
            if (tags == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在标签";
                return await Task.FromResult(result);
            }
            result.Result = tags;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntity<IEnumerable<Tag>>> GetTags()
        {
            var result = new ResultEntity<IEnumerable<Tag>>() { };
            var tags =  await _tagResponstory.GetAllTags();
            if (tags == null)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在标签";
                return await Task.FromResult(result);
            }
            result.Result = tags;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntity<PageEntity<IEnumerable<Tag>>>> GetTagsByPageAsync(PageEntityBase page)
        {
            var result = new ResultEntity<PageEntity<IEnumerable<Tag>>>() { };
            var tags = await _tagResponstory.GetTagsByPageAsync(page);
            if (tags.Result == null || tags.Result.Count() == 0)
            {
                result.Status = StatusCode.Error;
                result.Message = $"不存在标签";
                return await Task.FromResult(result);
            }
            result.Result = tags;
            return await Task.FromResult(result);
        }

        public async Task<ResultEntityBase> UpdateTag(Tag tag)
        {
            var result = new ResultEntityBase { };
            if (await _tagResponstory.UpdateTagsAsync(tag) != 0)
            {
                result.Status = StatusCode.Success;
                result.Message = "标签更新成功";
                return await Task.FromResult(result);
            }
            else
            {
                result.Status = StatusCode.Error;
                result.Message = "标签更新失败";
                return await Task.FromResult(result);
            }
        }
    }
}
