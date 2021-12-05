using System.Collections.Generic;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel
{
    public class CartDetails : IBaseMongoEntity
    {
        public ICollection<CartItem> CartItems { get; set; }
    }
}