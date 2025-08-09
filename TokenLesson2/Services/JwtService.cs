using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TokenLesson2.Common;
using TokenLesson2.Dtos.Response;
using TokenLesson2.Interface.Repository;
using TokenLesson2.Interface.Services;
using TokenLesson2.Models.User;

namespace TokenLesson2.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly JwtSettings _jwtSettings;

        public JwtService(IConfiguration configuration, IAuthRepository authRepository, IOptions<JwtSettings> jwtSettings)
        {
            _configuration = configuration;
            _authRepository = authRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public TokenDto GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key ?? throw new InvalidOperationException("JWT Key is not configured.")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("userName", user.UserName),
                new Claim("role", user.Role.ToString())
            };

            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(30);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(10);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: creds
            );

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration
            };
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
