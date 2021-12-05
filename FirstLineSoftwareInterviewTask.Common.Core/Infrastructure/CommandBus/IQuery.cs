using MediatR;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
        
    }
}