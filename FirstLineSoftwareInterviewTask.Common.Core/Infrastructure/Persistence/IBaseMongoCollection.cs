using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence
{
    public interface IBaseMongoCollection : IBaseMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}