using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel
{
    [BsonIgnoreExtraElements]
    public class UserCollection : IBaseMongoCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public CartDetails CartDetails { get; set; }
    }
}