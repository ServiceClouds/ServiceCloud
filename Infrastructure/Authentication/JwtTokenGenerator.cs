using Application.Abstractions.Authentication;
using Application.Abstractions.Commands.Login;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            var accessTokenExpiry = DateTime.UtcNow.AddHours(
                _jwtSettings.AccessTokenExpirationInHours);

            var refreshTokenExpiry = DateTime.UtcNow.AddMinutes(
                _jwtSettings.RefreshTokenExpirationMinutes);

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
                claims.Add(new Claim(
                    "BranchId",
                    user.BranchId.Value.ToString()));
            }

            var accessToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: accessTokenExpiry,
                signingCredentials: credentials);

            var refreshToken = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler()
                    .WriteToken(accessToken),

                RefreshToken = refreshToken,

                ExpiresAt = accessTokenExpiry,

                RefreshTokenExpiresAt = refreshTokenExpiry
            };
        }
    }
}