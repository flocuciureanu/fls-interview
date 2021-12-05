namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder
{
    public interface IResponseBuilder<out TResponse> : IBuilder where TResponse : class, IBaseResponse
    {
        IResponseBuilder<TResponse> Map<TSource>(TSource source);
        TResponse Build();
    }
}