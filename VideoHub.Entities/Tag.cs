using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities
{
    /// <summary>
    /// 标签表对应实体
    /// </summary>
    [SugarTable("ttag", "标签表")]
    public class Tag
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 标签名
        /// </summary>
        public string Name { get;set;}
        /// <summary>
        /// 观看次数
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// 逻辑删除标识(0:可用。1:隐藏。2:删除)
        /// </summary>
        public int State { get; set; }
    }
}
