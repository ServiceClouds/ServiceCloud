using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }

        public string TokenType => "Bearer";
    }
}
