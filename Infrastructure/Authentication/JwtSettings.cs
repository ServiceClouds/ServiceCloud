using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Authentication
{
    public sealed class JwtSettings
    {
        public string Key { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int AccessTokenExpirationInHours { get; set; }

        public int RefreshTokenExpirationMinutes { get; set; }
    }
}
