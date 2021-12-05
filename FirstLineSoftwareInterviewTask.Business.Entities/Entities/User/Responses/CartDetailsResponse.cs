using System.Collections.Generic;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses
{
    public class CartDetailsResponse : IBaseResponse
    {
        public ICollection<CartItemResponse> CartItems { get; set; }
        public double TotalPriceAmount { get; set; }
    }
}