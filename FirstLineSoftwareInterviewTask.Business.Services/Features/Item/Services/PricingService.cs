using System;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services
{
    public class PricingService : IPricingService
    {
        private readonly IItemService _itemService;

        public PricingService(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<double> GetItemPriceAsync(string itemId, int count)
        {
            var item = await _itemService.GetByIdAsync(itemId);
            if (item is null)
                return 0;
            
            return GetItemPrice(item.Pricing, count);
        }
        
        private static double GetItemPrice(PricingDocument itemPricing, int count)
        {
            var itemPrice = itemPricing.PriceAmount;
            
            switch (itemPricing.AvailableDiscount.DiscountType)
            {
                case DiscountType.None:
                    return Math.Round(itemPrice * count, 2);
                
                case DiscountType.MultiBuy_BOGSHP:
                {
                    if (count < 2)
                        return Math.Round(itemPrice, 2);

                    switch (count % 2)
                    {
                        case 0:
                            return Math.Round(itemPrice * count * 0.75, 2);

                        case > 0:
                            return Math.Round(itemPrice * (count - 1) * 0.75 + itemPrice, 2);
                    }
                }
                    break;
                case DiscountType.MultiBuy_BTGTF:
                    if (count < 3)
                        return Math.Round(itemPrice * count, 2);
                    
                    switch (count % 3)
                    {
                        case 0:
                            return Math.Round(itemPrice * (count / 3 * 2), 2);
                        
                        case > 0:
                            return Math.Round(itemPrice * (count / 3 * 2) + itemPrice * (count % 3), 2);
                    }
                    
                    break;
                default:
                    return 0;
            }

            return 0;
        }
        
        public async Task<double> GetCartTotalAsync(CartDetails userCartDetails)
        {
            double totalAmount = 0;

            foreach (var cartItem in userCartDetails.CartItems)
            {
                totalAmount += await GetItemPriceAsync(cartItem.ItemId, cartItem.Count);
            }

            return Math.Round(totalAmount, 2);
        }
    }
}