using MediatR;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        
    }
}