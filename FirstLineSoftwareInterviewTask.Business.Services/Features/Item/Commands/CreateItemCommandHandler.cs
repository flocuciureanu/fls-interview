using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Builders;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ICommandResult>
    {
        private readonly ICommandResultFactory _commandResultFactory;
        private readonly IItemService _itemService;
        private readonly IItemBuilder _itemBuilder;
        private readonly IResponseBuilder<ItemDetailsResponse> _responseBuilder;

        public CreateItemCommandHandler(ICommandResultFactory commandResultFactory, IItemService itemService,
            IItemBuilder itemBuilder, IResponseBuilder<ItemDetailsResponse> responseBuilder)
        {
            _commandResultFactory = commandResultFactory;
            _itemService = itemService;
            _itemBuilder = itemBuilder;
            _responseBuilder = responseBuilder;
        }

        public async Task<ICommandResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _itemBuilder
                .WithTitle(request.CreateItemRequest.Title)
                .WithPricingDocument(request.CreateItemRequest.Pricing)
                .Build();

            var addedItem = await _itemService.InsertOneAsync(item);

            return addedItem is null
                ? _commandResultFactory.Create(false, StatusCodes.Status400BadRequest, null, null)
                : _commandResultFactory.Create(true, StatusCodes.Status201Created, string.Empty,
                    _responseBuilder.Map(addedItem).Build());
        }
    }
}