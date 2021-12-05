using System.Collections.Generic;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.Cart
{
    public class CartDetailsBuilder : ICartDetailsBuilder
    {
        private CartDetails _cartDetails = new CartDetails();

        public CartDetailsBuilder()
        {
            Reset();
        }

        public ICartDetailsBuilder WithCartItems(ICollection<CartItem> cartItems)
        {
            _cartDetails.CartItems = cartItems;
            return this;
        }
        
        public CartDetails Build()
        {
            var cartDetails = this._cartDetails;
            Reset();

            return cartDetails;
        }
        
        private void Reset()
        {
            this._cartDetails = new CartDetails()
            {
                CartItems = new List<CartItem>()
            };
        }
    }
}