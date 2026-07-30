using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login
{
    public sealed record LoginCommand(
    string Email,
    string Password)
    : ICommand<LoginResponse>;
    }

