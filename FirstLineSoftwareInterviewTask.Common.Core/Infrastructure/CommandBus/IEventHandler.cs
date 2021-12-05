using MediatR.Pipeline;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface IEventHandler<in TCommand, in TResponse> : IRequestPostProcessor<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        
    }
}