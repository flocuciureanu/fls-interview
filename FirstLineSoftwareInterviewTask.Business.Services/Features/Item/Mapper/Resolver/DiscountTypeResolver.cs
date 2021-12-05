using AutoMapper;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Mapper.Resolver
{
    public class DiscountTypeResolver : IValueResolver<ItemCollection, ItemDetailsResponse, string>
    {
        public string Resolve(ItemCollection source, ItemDetailsResponse destination, string destMember, ResolutionContext context)
        {
            if (source.Pricing.AvailableDiscount?.DiscountType is null)
                return "No discount available for this item";
            
            var discount = source.Pricing.AvailableDiscount.DiscountType switch
            {
                DiscountType.None => "No discount available for this item",
                DiscountType.MultiBuy_BOGSHP => "Buy one get second half price",
                DiscountType.MultiBuy_BTGTF => "Buy two get third free",
                _ => "No discount available for this item"
            };

            return discount;
        }
    }
}