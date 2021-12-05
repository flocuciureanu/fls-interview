using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands
{
    public class GetAllItemsCommandHandler : IQueryHandler<GetAllItemsCommand, ICommandResult>
    {
        private readonly IItemService _itemService;
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IResponseBuilder<ItemDetailsResponse> _responseBuilder;

        public GetAllItemsCommandHandler(IItemService itemService, 
            ICommandResultFactory commandResultFactory,
            IResponseBuilder<ItemDetailsResponse> responseBuilder)
        {
            _itemService = itemService;
            _commandResultFactory = commandResultFactory;
            _responseBuilder = responseBuilder;
        }

        public async Task<ICommandResult> Handle(GetAllItemsCommand request, CancellationToken cancellationToken)
        {
            var items = await _itemService.GetAllAsync();

            var response = items.Select(x => 
                _responseBuilder
                    .Map(x)
                    .Build());
            
            return _commandResultFactory.Create(true, response);
        }
    }
}