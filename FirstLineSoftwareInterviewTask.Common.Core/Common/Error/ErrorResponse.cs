using System.Collections.Generic;

namespace FirstLineSoftwareInterviewTask.Common.Core.Common.Error
{
    public class ErrorResponse
    {
        public ICollection<ErrorItem> Errors { get; set; } = new List<ErrorItem>();
    }

    public class ErrorItem
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}