using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FichaCadastroAPI.Model
{
    public class FichaCadastroContextDB : DbContext
    {
        public DbSet<FichaModel> FichaModels { get; set; }
        public DbSet<DetalheModel> DetalheModels { get; set; }

        public FichaCadastroContextDB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalheModel>()
                        .HasOne(h => h.Ficha)
                        .WithMany(w => w.DetalheModels)
                        .Metadata
                        .DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<DetalheModel>()
                        .Property(p => p.DataCadastro)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<FichaModel>()
                        .Property(p => p.DataCadastro)
                        .HasDefaultValueSql("GETDATE()");

            //Efetuar carga no banco de dados
            modelBuilder.Entity<FichaModel>()
                .HasData(new FichaModel
                {
                    Id = 1,
                    DataCadastro = DateTime.Now,
                    DataNascimento = DateTime.Now,
                    Email = "teste1@email.com.br",
                    Nome = "teste umes"
                },
                new FichaModel
                {
                    Id = 2,
                    DataCadastro = DateTime.Now,
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Email = "teste2@email.com.br",
                    Nome = "teste dois"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}