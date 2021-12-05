using AutoMapper;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Mapper.Resolver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Mapper
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<CartDetails, CartDetailsResponse>()
                .ForMember(d => d.CartItems, e => e.MapFrom<CartItemsResolver>())
                .ForMember(d => d.TotalPriceAmount, e => e.MapFrom<CartTotalPriceResolver>());
        }
    }
}