using ETrade.Entity.Models.Entities;
using ETrade.Entity.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DAL.Context
{
    public class ETradeDbContext : IdentityDbContext<User,Role,int>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        // override onconfig ilk gelen 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=103S-10\MSSQLSERVER01; Database=ETradeDb;Trusted_Connection=true; TrustServerCertificate=true;");
        }
    }
}
