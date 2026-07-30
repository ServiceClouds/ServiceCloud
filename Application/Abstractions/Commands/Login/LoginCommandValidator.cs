using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Abstractions.Commands.Login
{
    public sealed class LoginCommandValidator
     : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
