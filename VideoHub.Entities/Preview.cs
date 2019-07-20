using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 预览表对应实体
    /// </summary>
    [SugarTable("tpreview", "预览表")]
    public class Preview
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 预览图地址
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// 视频Id
        /// </summary>
        public int VideoId { get; set; }
        /// <summary>
        /// 状态(0:可用。1:隐藏。2:删除)
        /// </summary>
        public int State { get; set; }
    }
}
