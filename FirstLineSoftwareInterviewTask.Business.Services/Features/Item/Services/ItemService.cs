using System.Collections.Generic;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services
{
    public class ItemService : IItemService
    {
        private readonly IDatabaseRepository<ItemCollection> _itemRepository;

        public ItemService(IDatabaseRepository<ItemCollection> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<List<ItemCollection>> GetAllAsync()
            => _itemRepository.GetAllAsync();

        public Task<ItemCollection> InsertOneAsync(ItemCollection itemToAdd)
            => _itemRepository.InsertOneAsync(itemToAdd);
        
        public Task<ItemCollection> GetByIdAsync(string id)
            => _itemRepository.GetByIdAsync(id);
    }
}