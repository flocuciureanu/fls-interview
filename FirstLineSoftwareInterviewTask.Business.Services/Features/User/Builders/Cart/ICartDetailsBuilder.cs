using System.Collections.Generic;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.Cart
{
    public interface ICartDetailsBuilder : IMongoEntityBuilder<CartDetails>
    {
        ICartDetailsBuilder WithCartItems(ICollection<CartItem> cartItems);
    }
}