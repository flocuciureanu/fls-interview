using System.Collections.Generic;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;
using MongoDB.Bson;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.User
{
    public class UserBuilder : IUserBuilder
    {
        private UserCollection _user = new UserCollection();

        public UserBuilder()
        {
            Reset();
        }

        public IUserBuilder WithFirstName(string firstName)
        {
            _user.FirstName = firstName;
            return this;
        }

        public IUserBuilder WithLastName(string lastName)
        {
            _user.LastName = lastName;
            return this;
        }

        public IUserBuilder WithEmailAddress(string emailAddress)
        {
            _user.EmailAddress = emailAddress;
            return this;
        }

        private void Reset()
        {
            this._user = new UserCollection()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                CartDetails = new CartDetails()
                {
                    CartItems = new List<CartItem>()
                }
            };
        }

        public UserCollection Build()
        {
            var user = this._user;
            Reset();

            return user;
        }
    }
}