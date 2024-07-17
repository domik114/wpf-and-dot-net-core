using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Gra> Gra { get; set; }
        public DbSet<ListaPowrotu> ListaPowrotu { get; set; }
        public DbSet<ListaZakazana> ListaZakazana { get; set; }
        public DbSet<ListaZyczen> ListaZyczen { get; set; }
    }
}
