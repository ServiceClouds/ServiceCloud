using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login
{
    public sealed record LoginCommand(
     int StaffId,
    int CompanyId,
    int BranchId)
    : ICommand<LoginResponse>;//mediatr return response
    }//only carries data from controller

