using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Common.Core.Settings.DataAccess;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence
{
    public class DatabaseRepository<T> : IDatabaseRepository<T> where T : IBaseMongoCollection
    {
        private readonly IMongoCollection<T> _collection;
        private readonly ILogger<DatabaseRepository<T>> _logger;

        public DatabaseRepository(IDatabaseSettings settings, IMongoDatabase database,
            ILogger<DatabaseRepository<T>> logger)
        {
            _logger = logger;
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T).Name, settings.DatabaseCollections));
        }

        private static string GetCollectionName(string name, DatabaseCollections databaseCollections)
        {
            return databaseCollections.GetType().GetProperties()
                .Where(propertyInfo => propertyInfo.Name.Contains(name))
                .Select(propertyInfo => propertyInfo.GetValue(databaseCollections, null)?.ToString())
                .FirstOrDefault();
        }

        public async Task<T> InsertOneAsync(T item)
        {
            if (item is null)
                return default;

            try
            {
                await _collection.InsertOneAsync(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return default;
            }

            return item;
        }

        public Task<T> GetByIdAsync(string id)
            => _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        public Task<List<T>> GetAllAsync()
            => _collection.Find(Builders<T>.Filter.Empty).ToListAsync();

        public Task<T> GetOneAsync(FilterDefinition<T> where)
            => _collection.Find(where).FirstOrDefaultAsync();

        public Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return _collection.FindOneAndUpdateAsync(filter, update,
                new FindOneAndUpdateOptions<T>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                });
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions options)
            => _collection.UpdateOneAsync(filter, update, options);

        public Task<DeleteResult> DeleteAsync(string id)
            => _collection.DeleteOneAsync(x => x.Id.Equals(id));
    }
}