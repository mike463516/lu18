using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.ViewModelEntity
{
    /// <summary>
    /// Token实体
    /// </summary>
    public class TokenEntity
    {
        /// <summary>
        /// 身份token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
