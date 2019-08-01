using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 视频分类关系类
    /// </summary>
    [SugarTable("tvideocategory", "视频分类关系表")]
    public class VideoCategory
    {
        public int VideoId { get; set; }
        public int CategoryId { get; set; }
        //[SugarColumn(IsIgnore = true)]
        //public Video Video { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Category Category { get; set; }
    }
}
