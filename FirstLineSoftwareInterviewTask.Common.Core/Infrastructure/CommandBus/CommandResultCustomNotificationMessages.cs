namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus
{
    public static class CommandResultCustomNotificationMessages
    {
        public const string InvalidItemCount = "Count must be greater than 0";
        public const string InvalidPriceAmount = "Price must be greater than 0";
        public const string EmptyUserCart = "User cart is currently empty";
        public const string UserCartSuccessfullyEmpty = "All items have been removed from the user's cart";
        public const string ItemNotInCart = "Item not currently in user cart";
        public const string InvalidEmailAddress = "The provided email address is not a valid one";
    }
}