using System.Collections.Generic;
using AutoMapper;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Factory;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder
{
    public class ResponseBuilder<TResponse> : IResponseBuilder<TResponse> where TResponse : class, IBaseResponse
    {
        private readonly IMapper _mapper;
        private readonly IResponseFactory<TResponse> _responseFactory;

        private IList<object> _mappedSources = new List<object>();

        public ResponseBuilder(IMapper mapper, IResponseFactory<TResponse> responseFactory)
        {
            _mapper = mapper;
            _responseFactory = responseFactory;
        }

        public IResponseBuilder<TResponse> Map<TSource>(TSource source)
        {
            _mappedSources.Add(source);
            
            return this;
        }
        
        public virtual TResponse Build()
        {
            var response = CreateResponse();

            foreach (var source in _mappedSources)
            {
                var typeMap = _mapper.ConfigurationProvider.FindTypeMapFor(source.GetType(), response.GetType());
                if (typeMap is null) 
                    continue;

                response = _mapper.Map(source, response);
            }

            Reset();
            
            return response;
        }

        private void Reset()
        {
            _mappedSources = new List<object>();
        }
        
        private TResponse CreateResponse()
        {
            var response = _responseFactory.Create();

            return response;
        }
    }
}