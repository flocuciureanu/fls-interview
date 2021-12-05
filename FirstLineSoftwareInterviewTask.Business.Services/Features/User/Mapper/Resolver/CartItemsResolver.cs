using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Mapper.Resolver
{
    public class CartItemsResolver : IValueResolver<CartDetails, CartDetailsResponse, ICollection<CartItemResponse>>
    {
        private readonly IPricingService _pricingService;
        private readonly IItemService _itemService;

        public CartItemsResolver(IPricingService pricingService, IItemService itemService)
        {
            _pricingService = pricingService;
            _itemService = itemService;
        }

        public ICollection<CartItemResponse> Resolve(CartDetails source, CartDetailsResponse destination, ICollection<CartItemResponse> destMember, ResolutionContext context)
        {
            var response = new List<CartItemResponse>();
            foreach (var cartItem in source.CartItems)
            {
                var itemFromDb = _itemService.GetByIdAsync(cartItem.ItemId).GetAwaiter().GetResult();
                
                response.Add( new CartItemResponse()
                {
                    ItemId = cartItem.ItemId,
                    Title = itemFromDb.Title, 
                    Count = cartItem.Count,
                    PriceAmount = _pricingService.GetItemPrice(cartItem.ItemId, cartItem.Count).GetAwaiter().GetResult()
                });
            }

            return response;
        }
    }
}