using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Data
{

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetService> PetServices { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<Xray> Xrays { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VetCase> VetCases { get; set; }
        public DbSet<CasePetService> CasePetServices { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<VetCase>()
                .HasKey(vc => new { vc.VetId, vc.CaseId });
           

            modelBuilder.Entity<CasePetService>()
                 .HasKey(cp => new { cp.CaseId, cp.PetServiceId });
           
            modelBuilder.SeedDataContext();
        }

    }
}
