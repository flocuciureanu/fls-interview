using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel
{
    public class CartItem : IBaseMongoEntity
    {
        public string ItemId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}