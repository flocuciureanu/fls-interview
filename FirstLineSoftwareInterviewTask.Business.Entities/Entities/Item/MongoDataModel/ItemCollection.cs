using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel
{
    [BsonIgnoreExtraElements]
    public class ItemCollection : IBaseMongoCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public PricingDocument Pricing { get; set; }
    }
}