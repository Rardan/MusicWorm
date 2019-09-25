using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicWorm.Models;

namespace MusicWorm.Data
{
    public class WormDbContext : IdentityDbContext<StoreUser>
    {
        public WormDbContext(DbContextOptions<WormDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Storage> Store { get; set; }

    }
}
