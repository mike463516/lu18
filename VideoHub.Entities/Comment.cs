using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{

    /// <summary>
    /// 视频评论表对应实体
    /// </summary>
    [SugarTable("tcomment", "视频评论表")]
    public class Comment
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 标签名
        /// </summary>
        public string CommentUser { get; set; }
        /// <summary>
        /// 视频Id
        /// </summary>
        public int VideoId { get; set; }
        /// <summary>
        /// 观看次数
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 逻辑删除标识(0:可用。1:隐藏。2:删除)
        /// </summary>
        public int State { get; set; }
    }
}
