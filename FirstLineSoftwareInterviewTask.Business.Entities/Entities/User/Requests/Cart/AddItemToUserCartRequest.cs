namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Requests.Cart
{
    public class AddItemToUserCartRequest
    {
        public string ItemId { get; set; }
        public int Count { get; set; }
    }
}