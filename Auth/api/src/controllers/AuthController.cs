using application.commands.auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.src.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILogger<AuthController> logger, IMediator mediator) : ControllerBase
    {
        private ILogger<AuthController> Logger { get; } = logger;
        private IMediator Mediator { get; } = mediator;

        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] AuthRequest auth)
        {
            Logger.LogInformation("Auth API is working!");
            var result = await Mediator.Send(auth);

            return await Task.FromResult(Ok(result));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            Logger.LogInformation("Auth API is working!");
            return await Task.FromResult(Ok("Auth API is working!"));
        }
    }
}