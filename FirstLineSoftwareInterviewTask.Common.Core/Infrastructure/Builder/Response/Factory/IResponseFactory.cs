namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Factory
{
    public interface IResponseFactory<out TResponse> where TResponse : IBaseResponse
    {
        TResponse Create();
    }
}