using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Relics.Models;

namespace Relics.Data
{
    public class RelicsDbContext : DbContext
    {
        public RelicsDbContext(DbContextOptions<RelicsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
