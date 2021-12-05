namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel
{
    public class PricingDocument
    {
        public double PriceAmount { get; set; }
        public DiscountDocument AvailableDiscount { get; set; }
    }
}