namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Contracts.V1
#pragma warning disable 1591
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Item
        {
            private const string ItemBase = Base + "/items";
            
            public const string Create = ItemBase;
            public const string GetAll = ItemBase;
        }
        
        public static class User
        {
            private const string UserBase = Base + "/users";
            
            public const string Create = UserBase;
            public const string AddToCart = UserBase + "/add-to-cart";
            public const string RemoveFromCart = UserBase + "/remove-from-cart";
            public const string EmptyCart = UserBase + "/empty-cart";
            public const string GetCartDetails = UserBase + "/cart-details";
        }
    }
}