using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.Item.Requests;
using FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Commands;
using FirstLineSoftwareInterviewTask.Hosting.WebApi.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#pragma warning disable 1591

namespace FirstLineSoftwareInterviewTask.Hosting.WebApi.Controllers
{
    /// <summary>
    /// This is the the items controller
    /// </summary>
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IMediator _mediator;

        public ItemController(ILogger<ItemController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        /// <summary>
        /// Creates a new item (Discount type: 0 = None, 1 = Buy one get second half price, 2 = Buy two get third free)
        /// </summary>
        [HttpPost]
        [Route(ApiRoutes.Item.Create)]
        public async Task<IActionResult> CreateItem(CreateItemRequest request)
        {
            var commandResult = await _mediator.Send(new CreateItemCommand()
            {
                CreateItemRequest = request
            });

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }    
        
        /// <summary>
        /// Returns all the items from the db
        /// </summary>
        [HttpGet]
        [Route(ApiRoutes.Item.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var commandResult = await _mediator.Send(new GetAllItemsCommand());

            if (!commandResult.Success)
            {
                _logger.LogError(commandResult.NotificationMessage);
            }

            return StatusCode(commandResult.StatusCode, commandResult);
        }
    }
}