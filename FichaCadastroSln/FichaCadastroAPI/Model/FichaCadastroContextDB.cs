using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FichaCadastroAPI.Model
{
    public class FichaCadastroContextDB : DbContext
    {
       public FichaCadastroContextDB(DbContextOptions options) : base(options)
       {
       }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}