using System.Collections.Generic;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services
{
    public interface IItemService
    {
        Task<List<ItemCollection>> GetAllAsync();
        Task<ItemCollection> InsertOneAsync(ItemCollection item);
        Task<ItemCollection> GetByIdAsync(string id);
    }
}