using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.Cart
{
    public class CartItemBuilder : ICartItemBuilder
    {
        private CartItem _cartItem = new CartItem();

        public CartItemBuilder()
        {
            Reset();
        }

        public ICartItemBuilder WithItemId(string id)
        {
            _cartItem.ItemId = id;
            return this;
        }

        public ICartItemBuilder WithCount(int count)
        {
            _cartItem.Count = count;
            return this;
        }

        public CartItem Build()
        {
            var cartItem = this._cartItem;
            Reset();

            return cartItem;
        }
        
        private void Reset()
        {
            this._cartItem = new CartItem();
        }
    }
}