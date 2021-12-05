using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder
{
    public interface IMongoEntityBuilder<out TCollection> : IBuilder where TCollection : class, IBaseMongoEntity
    {
        TCollection Build();
    }
}