using domain.interfaces;
using domain.interfaces.repository;
using domain.models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace application.commands.auth
{
    public class AuthHandler(ILogger<AuthHandler> logger, IAuthService authService, IUserWriteRepository repository) : IRequestHandler<AuthRequest, AuthResponse>
    {
        private readonly IUserWriteRepository _repository = repository;
        private readonly ILogger<AuthHandler> _logger = logger;
        private readonly IAuthService _authService = authService;
        private readonly IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

        public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var response = new AuthResponse();
            var expiration = _configuration.GetValue<int>("jwtToken:expirationMinutes");
            var expirationDateTime = DateTime.UtcNow.AddMinutes(expiration);
            var authentication = new Authentication
            {
                User = request.User,
                Password = request.Password
            };

            var token = _authService.GenerateToken(authentication, _configuration.GetValue<string>("jwtToken:key"), expirationDateTime);
            var passwordHash = await _repository.GetPasswordHash(authentication, cancellationToken);

            if (string.IsNullOrEmpty(passwordHash))
            {
                _logger.LogWarning("Usuário ou senha inválidos!");
                return response;
            }

            var authValidation = BCrypt.Net.BCrypt.Verify(request.Password, passwordHash);

            if (authValidation)
            {
                _logger.LogInformation("Usuário autenticado com sucesso!");
                response = new AuthResponse { Token = token };
                return response;
            }

            return response;
        }
    }
}