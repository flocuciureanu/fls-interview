using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.Cart
{
    public interface ICartItemBuilder : IMongoEntityBuilder<CartItem>
    {
        ICartItemBuilder WithItemId(string id);
        ICartItemBuilder WithCount(int count);
    }
}