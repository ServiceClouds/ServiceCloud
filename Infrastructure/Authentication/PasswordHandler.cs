using Application.Abstractions.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;



namespace Infrastructure.Authentication
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password + salt);

            using var sha256 = SHA256.Create();

            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(
            string password,
            string passwordHash,
            string salt)
        {
            var hashedPassword = HashPassword(password, salt);

            return hashedPassword == passwordHash;
        }
    }
}
