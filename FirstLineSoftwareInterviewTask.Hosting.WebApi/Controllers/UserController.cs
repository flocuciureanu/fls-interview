using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.Requests.Cart;
using FirstLineSoftwareInterviewTask.Business.Services.Features.User.Commands.Cart;
using FirstLineSoftwareInterviewTask.Hosting.WebApi.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Controllers
{
    /// <summary>
    /// This is the the users controller
    /// </summary>
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        private const string TestUserId = "61abb304eb1388bd9e5b6e60";
        
        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // Used this endpoint just to create a test user
        
        ///// <summary>
        ///// Creates a new user
        ///// </summary>
        // [HttpPost]
        // [Route(ApiRoutes.User.Create)]
        // public async Task<IActionResult> CreateUser(CreateUserRequest request)
        // {
        //     var commandResult = await _mediator.Send(new CreateUserCommand()
        //     {
        //         CreateUserRequest = request
        //     });
        //
        //     if (!commandResult.Success)
        //     {
        //         _logger.LogError(commandResult.NotificationMessage);
        //     }
        //
        //     return StatusCode(commandResult.StatusCode, commandResult);
        // }

        /// <summary>
        /// Adds a specific amount of a specific item to the cart
        /// </summary>
        [HttpPatch]
        [Route(ApiRoutes.User.AddToCart)]
        public async Task<IActionResult> AddItemToUserCart(AddItemToUserCartRequest request)
        {
            var commandResult = await _mediator.Send(new AddItemToUserCartCommand()
            {
                UserId = TestUserId,
                ItemId = request.ItemId,
                Count = request.Count
            });

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }

        /// <summary>
        /// Removes a specific amount of a specific item from the cart
        /// </summary>
        [HttpPatch]
        [Route(ApiRoutes.User.RemoveFromCart)]
        public async Task<IActionResult> RemoveItemFromUserCart(RemoveItemFromUserCartRequest request)
        {
            var commandResult = await _mediator.Send(new RemoveItemFromCartCommand()
            {
                UserId = TestUserId,
                ItemId = request.ItemId,
                Count = request.Count
            });

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }
        
        /// <summary>
        /// Empties the user cart
        /// </summary>
        [HttpPatch]
        [Route(ApiRoutes.User.EmptyCart)]
        public async Task<IActionResult> EmptyCard()
        {
            var commandResult = await _mediator.Send(new EmptyCartCommand()
            {
                UserId = TestUserId
            });

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }
        
        /// <summary>
        /// Gets the user's cart details
        /// </summary>
        [HttpGet]
        [Route(ApiRoutes.User.GetCartDetails)]
        public async Task<IActionResult> GetUserCartDetails()
        {
            var commandResult = await _mediator.Send(new GetCartDetailsCommand()
            {
                UserId = TestUserId
            });

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }
    }
}