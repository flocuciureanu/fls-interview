namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests
{
    public class CreateItemRequest
    {
        public string Title { get; set; }
        public PricingRequest Pricing { get; set; }
    }
}