using FirstLineSoftwareInterviewTask.Common.Core.Common.Error;
using Microsoft.AspNetCore.Http;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public class CommandResult : ICommandResult
    {
        public object Value { get; set; }

        public bool Success { get; set; }

        public string NotificationMessage { get; set; }
        
        public ErrorResponse Errors { get; set; }

        public int StatusCode { get; set; } = StatusCodes.Status200OK;
    }
}