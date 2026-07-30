using Application.Abstractions.Authentication;

using Application.Abstractions.Commands.Login;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public LoginResponse GenerateToken(AuthenticatedUser user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(
                _jwtSettings.AccessTokenExpirationInHours);

            var claims = new List<Claim>
            {
               
                new Claim("StaffId", user.StaffId.ToString()),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("Email", user.Email),
                new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()),
                new Claim("HasEnterpriseRole", user.HasEnterpriseRole.ToString())
            };

            if (user.BranchId.HasValue)
            {
                claims.Add(new Claim("BranchId", user.BranchId.Value.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expires
            };
        }
    }
}

