using System;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Factory
{
    public class ResponseFactory<TResponse> : IResponseFactory<TResponse> where TResponse : IBaseResponse
    {
        public TResponse Create()
        {
            var response = Activator.CreateInstance<TResponse>();

            return response;
        }
    }
}