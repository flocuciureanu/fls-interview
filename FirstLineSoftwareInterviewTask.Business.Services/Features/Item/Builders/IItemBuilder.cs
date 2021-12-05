using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Builders
{
    public interface IItemBuilder : IMongoEntityBuilder<ItemCollection>
    {
        IItemBuilder WithTitle(string title);
        IItemBuilder WithPricingDocument(PricingRequest pricingRequest);
    }
}