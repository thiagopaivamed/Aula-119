using System;
using System.Collections.Generic;
using System.Text;
using GerUsuario.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GerUsuario.Models
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NivelAcesso> NiveisAcessos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
