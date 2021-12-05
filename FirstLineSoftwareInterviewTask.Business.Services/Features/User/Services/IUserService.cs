using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using MongoDB.Driver;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services
{
    public interface IUserService
    {
        Task<UserCollection> InsertOneAsync(UserCollection userToAdd);
        Task<UserCollection> GetUserByIdAsync(string id);
        Task<UserCollection> FindOneAndUpdateAsync(FilterDefinition<UserCollection> filter, UpdateDefinition<UserCollection> update);
    }
}