using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);

        bool VerifyPassword(
            string password,
            string passwordHash,
            string salt);
    }
}
