using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data

{
    public interface IApplicationDbContext:IDbContext
{
    DbSet<T> GetDbSet<T>() where T : class;



}
}
