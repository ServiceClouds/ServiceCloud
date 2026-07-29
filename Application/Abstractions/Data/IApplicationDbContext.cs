using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data

{
    public interface IApplicationDbContext
{
    DbSet<T> GetDbSet<T>() where T : class;



}
}
