using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 流量表对应实体
    /// </summary>
    [SugarTable("tvideo", "流量表")]
    public class ViewLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 访问路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 访问的资源类型 0网页，1视频
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 访问人
        /// </summary>
        public string Viewer { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
