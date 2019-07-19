using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.CommonEntity
{
    public class PageEntity<TResult> : PageEntityBase where TResult:class
    {
        public TResult Result { get; set; }
    }
    public class PageEntityBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
