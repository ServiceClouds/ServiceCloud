using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login.VefityLogin { 
public sealed record VerifyLoginCommand(
    int StaffId,
    int CompanyId,
    string Password
) : ICommand<VerifyLoginResponse>; }