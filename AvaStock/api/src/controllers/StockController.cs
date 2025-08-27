using application.queries.product.search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace api.src.controllers
{
    [Controller]
    [Route("v1/api/[controller]")]
    public class StockController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator Mediator = mediator;

    [HttpGet]
    [Authorize]
    public async Task<SearchProductResponse> Get()
        {
            var result = await Mediator.Send(new SearchProductRequest());

            return result;
        }
    }
}