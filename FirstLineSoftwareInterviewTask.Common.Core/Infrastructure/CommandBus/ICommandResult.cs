using FirstLineSoftwareInterviewTask.Common.Core.Common.Error;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface ICommandResult
    {
        bool Success { get; set; }

        object Value { get; set; }

        string NotificationMessage { get; set; }

        ErrorResponse Errors { get; set; }
        
        int StatusCode { get; set; }
    }
}