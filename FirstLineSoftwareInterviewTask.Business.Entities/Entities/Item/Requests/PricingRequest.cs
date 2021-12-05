using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests
{
    public class PricingRequest
    {
        public double PriceAmount { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}