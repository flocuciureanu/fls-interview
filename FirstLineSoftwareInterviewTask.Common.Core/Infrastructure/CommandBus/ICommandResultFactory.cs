using FirstLineSoftwareInterviewTask.Common.Core.Common.Error;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface ICommandResultFactory
    {
        CommandResult Create(bool success, object value = null, ErrorResponse errors = null);

        CommandResult Create(bool success, string message, object value = null, ErrorResponse errors = null);
        
        CommandResult Create(bool success, int statusCode, string message = null, object value = null, ErrorResponse errors = null);
    
        CommandResult Create(bool success, int statusCode, string message, ErrorResponse errors);
    }
}