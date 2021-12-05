using System;
using System.Linq;
using AutoMapper;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Responses;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Mapper.Resolver
{
    public class CartTotalPriceResolver : IValueResolver<CartDetails, CartDetailsResponse, double>
    {
        public double Resolve(CartDetails source, CartDetailsResponse destination, double destMember, ResolutionContext context)
        {
            return Math.Round(destination.CartItems.Sum(x => x.PriceAmount), 2);
        }
    }
}