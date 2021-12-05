using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses
{
    public class CartItemResponse : CartItem, IBaseResponse
    {
        public double PriceAmount { get; set; }
    }
}