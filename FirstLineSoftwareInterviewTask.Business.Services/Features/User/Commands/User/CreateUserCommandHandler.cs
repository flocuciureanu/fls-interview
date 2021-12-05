using System.Threading;
using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Builders.User;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Services;
using FirstLineSoftwareInterviewTask.Common.Core.Infrastructure.CommandBus;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ICommandResult>
    {
        private readonly IUserService _userService;
        private readonly IUserBuilder _userBuilder;
        private readonly ICommandResultFactory _commandResultFactory;

        public CreateUserCommandHandler(IUserService userService, IUserBuilder userBuilder, ICommandResultFactory commandResultFactory)
        {
            _userService = userService;
            _userBuilder = userBuilder;
            _commandResultFactory = commandResultFactory;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = _userBuilder
                .WithFirstName(request.CreateUserRequest.FirstName)
                .WithLastName(request.CreateUserRequest.LastName)
                .WithEmailAddress(request.CreateUserRequest.EmailAddress)
                .Build();

            var addedUser = await _userService.InsertOneAsync(userToAdd);

            return addedUser is null
                ? _commandResultFactory.Create(false, StatusCodes.Status400BadRequest)
                : _commandResultFactory.Create(true, StatusCodes.Status201Created);
        }
    }
}