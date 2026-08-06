using Domain.Entities.ServiceCloud;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff?> GetByEmailAsync(string email);
    }
}
