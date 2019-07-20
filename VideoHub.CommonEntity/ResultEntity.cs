using System;

namespace VideoHub.CommonEntity
{
    public class ResultEntity<TResult> : ResultEntityBase where TResult : class
    {
        public TResult Result { get; set; }
    }
    public class ResultEntityBase
    {
        public string Message { get; set; } = string.Empty;
        public StatusCode Status { get; set; } = StatusCode.Success;
    }
    public enum StatusCode
    {
        Success,
        Warn,
        Error
    }
}
