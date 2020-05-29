using System;
using Microsoft.EntityFrameworkCore;

namespace Paschoalotto.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }
        public DbSet<UsuarioLogin> UsuarioLogin { get; set; }
        public DbSet<ColaboradorLogin> ColaboradorLogin { get; set; }
        public DbSet<Contrato> Contrato { get; set; }

        public DbSet<ConfiguracaoTaxas> ConfiguracaoTaxas { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boleto>()
                        .HasOne(c => c.Contrato)
                        .WithMany(b => b.Boletos)
                        .HasForeignKey(b => b.IdContrato)
                        .IsRequired(false);      
        }


    }
}