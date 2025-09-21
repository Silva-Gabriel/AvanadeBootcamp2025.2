using application.queries.product.search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using application.queries.product.details;
using domain.models.product.parameters;
using application.commands.product.create;

namespace api.src.controllers
{
    [Controller]
    [Route("v1/[controller]/api")]
    public class StockController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator Mediator = mediator;

        [HttpGet("search")]
        [Authorize]
        public async Task<SearchProductResponse> SearchProduct([FromQuery] SearchProductParameter parameters)
        {
            var result = await Mediator.Send(new SearchProductRequest
            {
                Id = parameters.Id,
                Category = parameters.Category,
                Description = parameters.Description,
                Name = parameters.Name,
                Supplier = parameters.Supplier
            });

            return result;
        }

        [HttpGet("details")]
        [Authorize]
        public async Task<GetProductDetailsResponse> GetProductDetails(long productId)
        {
            var result = await Mediator.Send(new GetProductDetailsRequest { ProductId = productId });

            return result;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateProductRequest request)
        {
            var result = Mediator.Send(request);
            return Ok(result);
        }
    }
}