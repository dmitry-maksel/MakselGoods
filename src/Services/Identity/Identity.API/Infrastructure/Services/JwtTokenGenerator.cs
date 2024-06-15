using Identity.API.Core.Data;
using Identity.API.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Infrastructure.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private readonly JwtSettings _settings = new JwtSettings();

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!));
            configuration.Bind(JwtSettings.SectionKey, _settings);
        }

        public string GenerateToken(ApplicationUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, user.UserName!),
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials,
                Audience = _settings.Audience,
                Issuer = _settings.Issuer,
                IssuedAt = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
