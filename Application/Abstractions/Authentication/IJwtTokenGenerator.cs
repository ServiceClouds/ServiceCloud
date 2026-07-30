using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands.Login;
using Domain.Entities;
namespace Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
         LoginResponse GenerateToken(AuthenticatedUser User);
    }
}
