using domain.models;

namespace domain.interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(Authentication authRequest, string secretKey, DateTime expiration);
    }
}