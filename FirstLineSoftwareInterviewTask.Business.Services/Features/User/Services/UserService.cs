using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Persistence;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseRepository<UserCollection> _userRepository;

        public UserService(IDatabaseRepository<UserCollection> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserCollection> InsertOneAsync(UserCollection userToAdd)
            => _userRepository.InsertOneAsync(userToAdd);

        public Task<UserCollection> GetUserByIdAsync(string id)
            => _userRepository.GetByIdAsync(id);
        
        public Task<UserCollection> FindOneAndUpdateAsync(FilterDefinition<UserCollection> filter, UpdateDefinition<UserCollection> update)
            => _userRepository.FindOneAndUpdateAsync(filter, update);
    }
}