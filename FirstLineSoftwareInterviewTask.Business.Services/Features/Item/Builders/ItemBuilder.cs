using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests;
using MongoDB.Bson;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Builders
{
    public class ItemBuilder : IItemBuilder
    {
        private ItemCollection _item = new ItemCollection();

        public ItemBuilder()
        {
            Reset();
        }

        public IItemBuilder WithTitle(string title)
        {
            _item.Title = title;
            return this;
        }

        public IItemBuilder WithPricingDocument(PricingRequest pricingRequest)
        {
            _item.Pricing = new PricingDocument()
            {
                PriceAmount = pricingRequest.PriceAmount,
                AvailableDiscount = new DiscountDocument()
                {
                    DiscountType = pricingRequest.DiscountType
                }
            };
            
            return this;
        }
        
        private void Reset()
        {
            _item = new ItemCollection()
            {
                Id = ObjectId.GenerateNewId().ToString()
            };
        }
        
        public ItemCollection Build()
        {
            var item = _item;
            Reset();

            return item;
        }
    }
}