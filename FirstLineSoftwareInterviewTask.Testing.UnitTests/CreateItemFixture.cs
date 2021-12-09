using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Builders;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response.Builder;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace FirstLineSoftwareInterviewTask.Testing.UnitTests
{
    public class CreateItemFixture
    {
        private Mock<ICommandResultFactory> _commandResultFactoryMock;
        private Mock<IItemService> _itemServiceMock;
        private Mock<IItemBuilder> _itemBuilderMock;
        private Mock<IResponseBuilder<ItemDetailsResponse>> _itemBuilderResponseMock;

        public CreateItemFixture()
        {
            _commandResultFactoryMock = new Mock<ICommandResultFactory>();
            _itemServiceMock = new Mock<IItemService>();
            _itemBuilderMock = new Mock<IItemBuilder>();
            _itemBuilderResponseMock = new Mock<IResponseBuilder<ItemDetailsResponse>>();
        }

        [Fact]
        public async Task ValidateAsync_ValidRequest_ReturnsTrue()
        {
            //Arrange
            var validCreateItemCommandRequest = new CreateItemCommand()
            {
                CreateItemRequest = new CreateItemRequest()
                {
                    Title = "Item 1",
                    Pricing = new PricingRequest() { PriceAmount = 2, DiscountType = DiscountType.None }
                }
            };

            var validator = new CreateItemCommandValidator();
            
            //Act
            var validationResult = await validator.ValidateAsync(validCreateItemCommandRequest);
            
            //Assert
            Assert.True(validationResult.IsValid);
        }

        [Fact]
        public async Task ValidateAsync_InvalidRequest_ReturnsFalseAndNumberOfErrors()
        {
            //Arrange
            //2 validation errors: empty title and negative price amount
            var invalidCreateItemCommandRequest = new CreateItemCommand()
            {
                CreateItemRequest = new CreateItemRequest()
                {
                    Title = string.Empty,
                    Pricing = new PricingRequest() { PriceAmount = -2, DiscountType = DiscountType.None }
                }
            };

            var validator = new CreateItemCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(invalidCreateItemCommandRequest);
            var actualErrorsCount = validationResult.Errors.Count;

            //Assert
            Assert.Equal(2, actualErrorsCount);
            Assert.False(validationResult.IsValid);
        }

        [Fact]
        public async Task Handle_InsertAsyncFails_Returns400BadRequestResult()
        {
            //Arrange
            var createItemCommand = new CreateItemCommand()
            {
                CreateItemRequest = new CreateItemRequest()
                {
                    Title = "Item 1",
                    Pricing = new PricingRequest() { PriceAmount = 2, DiscountType = DiscountType.None }
                }
            };

            var item = new ItemCollection()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Title = "Item 1",
                Pricing = new PricingDocument()
                    { PriceAmount = 2, AvailableDiscount = new DiscountDocument() { DiscountType = DiscountType.None } }
            };

            ItemCollection? nullCreatedItem = null;
            
            var createItemCommandHandler = new CreateItemCommandHandler(_commandResultFactoryMock.Object,
                _itemServiceMock.Object, 
                _itemBuilderMock.Object, 
                _itemBuilderResponseMock.Object);

            var errorCommandResult = new CommandResult() { StatusCode = StatusCodes.Status400BadRequest };
            
            _itemBuilderMock.Setup(x => x.WithTitle(createItemCommand.CreateItemRequest.Title)).Returns(_itemBuilderMock.Object);
            _itemBuilderMock.Setup(x => x.WithPricingDocument(createItemCommand.CreateItemRequest.Pricing)).Returns(_itemBuilderMock.Object);
            _itemBuilderMock.Setup(x => x.Build()).Returns(item);
            
            _itemServiceMock.Setup(x => x.InsertOneAsync(item)).ReturnsAsync(nullCreatedItem);
            
            _commandResultFactoryMock.Setup(x => x.Create(false, StatusCodes.Status400BadRequest, null, null))
                .Returns(errorCommandResult);
            
            //Act
            var actual = await createItemCommandHandler.Handle(createItemCommand, CancellationToken.None);
            
            //Assert
            Assert.Equal(errorCommandResult, actual);
        }
    }
}