using MediatR;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        
    }
}