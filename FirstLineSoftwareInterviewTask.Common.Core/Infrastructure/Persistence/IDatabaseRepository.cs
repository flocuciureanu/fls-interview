using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence
{
    public interface IDatabaseRepository<T> where T : IBaseMongoCollection
    {
        Task<T> InsertOneAsync(T item);
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options);
        Task<DeleteResult> DeleteAsync(string id);
    }
}