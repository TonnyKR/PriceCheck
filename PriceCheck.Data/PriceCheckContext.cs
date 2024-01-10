using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;
using PriceCheck.Data.Configurations;
using PriceCheck.Data.Entities;


namespace PriceCheck.Data
{
    public class PriceCheckContext : DbContext
    
    {
        public PriceCheckContext()
        {
        }
        public PriceCheckContext(DbContextOptions<PriceCheckContext> options)
           : base(options)
        {
        }

        public DbSet<ATB> ATB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = typeof(ConfigurationsAssemblyMarker).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
