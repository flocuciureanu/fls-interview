using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.Response;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses
{
    public class ItemDetailsResponse : IBaseResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double PriceAmount { get; set; }
        public string DiscountType { get; set; }
    }
}