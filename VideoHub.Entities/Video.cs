using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 视频表对应实体
    /// </summary>
    [SugarTable("tvideo", "视频表")]
    public class Video
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Guid 用作路径
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 视频名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 视频标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 视频原文件名，带文件扩展名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 视频加密密匙
        /// </summary>
        public string SecertKey { get; set; }
        /// <summary>
        /// 点赞
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// 讨厌
        /// </summary>
        public int HateCount { get; set; }
        /// <summary>
        /// 观看次数
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 逻辑删除标识(0:可用。1:隐藏。2:删除)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 更新用户
        /// </summary>
        public string UpdateUser { get; set; }
    }
}
