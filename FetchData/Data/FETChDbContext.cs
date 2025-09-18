using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchData.Data
{
    public class FETChDbContext : IdentityDbContext
    {
        public FETChDbContext(DbContextOptions<FETChDbContext> options)
        : base(options)
        {
        }
    }
}
