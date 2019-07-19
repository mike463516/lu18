using SqlSugar;
using System;

namespace VideoHub.AccountEntity
{
    /// <summary>
    ///用户信息实体类
    /// </summary>
    [SugarTable("tuser","用户表")]
    public class User
    {
        /// <summary>
        /// 主鍵
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 头像图片路径
        /// </summary>
        public string HeadImageSrc { get; set; }
        /// <summary>
        /// 用户类型 (用户或者管理员或者访客)       
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 逻辑状态标识(0:可用。1:隐藏。2:删除) |
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
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
