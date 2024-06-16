using BlogAgro.Domain.Admin;

namespace BlogAgro.Services.JwtServices
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Account account);
        public int? ValidateJwtToken(string? token);
    }
}
