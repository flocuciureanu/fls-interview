using FirstLineSoftwareInterviewTask.Common.Core.Common.Error;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public class CommandResultFactory : ICommandResultFactory
    {
        public CommandResult Create(bool success, object value = null, ErrorResponse errors = null)
        {
            return new CommandResult { Value = value, Success = success, Errors = errors };
        }

        public CommandResult Create(bool success, string message, object value = null, ErrorResponse errors = null)
        {
            return new CommandResult { NotificationMessage = message, Success = success, Value = value, Errors = errors };
        }

        public CommandResult Create(bool success, int statusCode, string message = null, object value = null, ErrorResponse errors = null)
        {
            return new CommandResult { NotificationMessage = message, Success = success, Value = value, StatusCode = statusCode, Errors = errors };
        }
        
        public CommandResult Create(bool success, int statusCode, string message, ErrorResponse errors)
        {
            return new CommandResult { Success = success, StatusCode = statusCode, NotificationMessage = message, Errors = errors };
        }
    }
}