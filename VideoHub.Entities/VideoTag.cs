using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 视频标签关系类
    /// </summary>
    [SugarTable("tvideotag", "视频标签关系表")]
    public class VideoTag
    {
        public int VideoId { get; set; }
        public int TagId { get; set; }
        //[SugarColumn(IsIgnore = true)]
        //public Video Video { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Tag Tag { get; set; }
    }
}
