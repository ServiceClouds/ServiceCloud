using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
namespace Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Staff staff);
    }
}
