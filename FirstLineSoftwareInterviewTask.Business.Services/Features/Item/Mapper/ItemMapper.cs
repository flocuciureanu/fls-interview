using AutoMapper;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Mapper.Resolver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Mapper
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<ItemCollection, ItemDetailsResponse>()
                .ForMember(d => d.PriceAmount, e => e.MapFrom(s => s.Pricing.PriceAmount))
                .ForMember(d => d.DiscountType, e => e.MapFrom<DiscountTypeResolver>());
        }
    }
}