using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.Builder.MongoEntityBuilder;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.User
{
    public interface IUserBuilder : IMongoEntityBuilder<UserCollection>
    {
        IUserBuilder WithFirstName(string firstName);
        IUserBuilder WithLastName(string lastName);
        IUserBuilder WithEmailAddress(string emailAddress);
    }
}