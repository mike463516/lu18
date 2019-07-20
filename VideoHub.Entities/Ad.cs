using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 广告表对应实体
    /// </summary>
    [SugarTable("tad", "广告表")]
    public class Ad
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// 观看次数
        /// </summary>
        public string ViewCount { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidePeriods{ get; set; }
        /// <summary>
        /// 逻辑删除标识(0:可用。1:隐藏。2:删除)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 广告区域
        /// </summary>
        public int Area { get; set; }
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
