using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasData(
                new Animal { Id = 1, Name = "Dog"},
                new Animal { Id = 2, Name = "Cat" },
                new Animal { Id = 3, Name = "Parrot" },
                new Animal { Id = 4, Name = "Rabbit" });

            modelBuilder.Entity<Case>()
                .HasData(
                new Case
                {
                    Id = 1,
                    Name = "Dog with anorexia, lethargy, and vomiting",
                    Diagnosis = "The dog has been anorectic, lethargic, and vomiting for several days.",
                    Date = new DateTime(2018, 10, 3),
                    Description = "During a visit to the referring veterinarian 2 months ago, " +
                    "blood work revealed increased liver enzymes. " +
                    "The dog was started on amoxicillin and a liver support supplement but did not improve.",
                    PetId = 1
                },
                new Case
                {
                    Id = 2,
                    Name = "Cat stomach virus",
                    Diagnosis = "refers to inflammation of the gastrointestinal tract.",
                    Date = new DateTime(2019, 9, 5),
                    Description = "It can be caused by infection with bacteria, viruses, parasites, medications, or even new foods.",
                    PetId = 2
                },
                new Case
                {
                    Id = 3,
                    Name = "Routine examination",
                    Diagnosis = "Routine examination",
                    Date = new DateTime(2020, 9, 5),
                    Description = "Routine examination",
                    PetId = 3
                },
                new Case
                {
                    Id = 4,
                    Name = "Routine examination",
                    Diagnosis = "Routine examination",
                    Date = new DateTime(2020, 9, 5),
                    Description = "Routine examination",
                    PetId = 4
                }); 

            modelBuilder.Entity<Control>()
               .HasData(
               new Control { Id = 1, Name = "Dog with anorexia", Date = new DateTime(2018, 8, 6), Description = "Dog control after a week", CaseId = 1 },
               new Control { Id = 2, Name = "Cat with stomach problem", Date = new DateTime(2019, 9, 12), Description = "Dog control after a week", CaseId = 2 });

            modelBuilder.Entity<Owner>()
              .HasData(
              new Owner { Id = 1, FirstName = "Joe", LastName = "Harris", IdCard = "11AX7", Address = "Test Address", Email = "joeharris@mail.com", Phone = 0653311009 },
              new Owner { Id = 2, FirstName = "Denis", LastName = "Leon", IdCard = "23NZ0", Address = "Test Address2", Email = "leon@mail.com", Phone = 0630098754 },
              new Owner { Id = 3, FirstName = "Neil", LastName = "Carson", IdCard = "50XE1", Address = "Test Address3", Email = "neilc@mail.com", Phone = 0631002003 },
              new Owner { Id = 4, FirstName = "Tom", LastName = "Patterson", IdCard = "66OP0", Address = "Test Address4", Email = "tp4@mail.com", Phone = 0649987632 });

            modelBuilder.Entity<Pet>()
                .HasData(
                new Pet { Id = 1, Name = "Rex", Sex = Pet.Gender.male, Year = new DateTime(2015, 1, 1), RaceId = 1, OwnerId = 1 },
                new Pet { Id = 2, Name = "Jenkins", Sex = Pet.Gender.male, Year = new DateTime(2011, 3, 7), RaceId = 6, OwnerId = 3 },
                new Pet { Id = 3, Name = "Coco", Sex = Pet.Gender.male, Year = new DateTime(2010, 2, 2), RaceId = 11,  OwnerId = 2 },
                new Pet { Id = 4, Name = "Tia", Sex = Pet.Gender.female, Year = new DateTime(2020, 3, 3), RaceId = 13, OwnerId = 4 });

            modelBuilder.Entity<PetService>()
                .HasData(
                new PetService { Id = 1, Name = "Test1", Price = 100, Description = "test1 service" },
                new PetService { Id = 2, Name = "Test2", Price = 300, Description = "test2 service" },
                new PetService { Id = 3, Name = "Test3", Price = 500, Description = "test3 service" },
                new PetService { Id = 4, Name = "Test4", Price = 1000, Description = "test4 service" });

            modelBuilder.Entity<Race>()
                .HasData(
                new Race { Id = 1, Name = "German Shepherd Dog", AnimalId = 1 },
                new Race { Id = 2, Name = "Jack Russell", AnimalId = 1 },
                new Race { Id = 3, Name = "Rottweiler", AnimalId = 1 },
                new Race { Id = 4, Name = "Boxer", AnimalId = 1 },
                new Race { Id = 5, Name = "Persian", AnimalId = 2 },
                new Race { Id = 6, Name = "Siamese", AnimalId = 2 },
                new Race { Id = 7, Name = "Maine Coon", AnimalId = 2 },
                new Race { Id = 8, Name = "Ragdoll", AnimalId = 2 },
                new Race { Id = 9, Name = "Ara", AnimalId = 3 },
                new Race { Id = 10, Name = "Cockatoo", AnimalId = 3 },
                new Race { Id = 11, Name = "Cockatiel", AnimalId = 3 },
                new Race { Id = 12, Name = "Macaw", AnimalId = 3 },
                new Race { Id = 13, Name = "American", AnimalId = 4 },
                new Race { Id = 14, Name = "Alexandria", AnimalId = 4 },
                new Race { Id = 15, Name = "Chaudry", AnimalId = 4 });

            //modelBuilder.Entity<Therapy>()
            //    .HasData(
                //new Therapy { Id = 1, Drug = "Drug for anorexia", Date = new DateTime(2018, 1, 6), Description = "Take this drug for one week", CaseId = 1 },
                //new Therapy { Id = 2, Drug = "Drug for stomach problems", Date = new DateTime(2019, 9, 5), Description = "Take this drug for one week", CaseId = 2 });

            modelBuilder.Entity<User>()
                .HasData(
                new User { Id = 1, Username = "TestUser1", Email = "testuser1@mail.com", Password = "Test1234*", RoleId = 1 },
                new User { Id = 2, Username = "TestUser2", Email = "testuser@mail.com", Password = "1234Test*", RoleId = 2 });

            modelBuilder.Entity<Vet>()
                .HasData(
                new Vet { Id = 1, FirstName = "Will", LastName = "Martin", IdCard = "44CM9", Address = "Test Vet Address", Email = "wmartin@mail.com", Phone = 0668764230 },
                new Vet { Id = 2, FirstName = "Tim", LastName = "Thomas", IdCard = "79MR0", Address = "Test Vet Address2", Email = "tt40@mail.com", Phone = 0668764231 },
                new Vet { Id = 3, FirstName = "John", LastName = "Tylor", IdCard = "21YU1", Address = "Test Vet Address3", Email = "johntt@mail.com", Phone = 0668764232 });

            //modelBuilder.Entity<Xray>()
            //    .HasData(
            //    new Xray { Id = 1, Url = "testanorexiaxray", Date = new DateTime(2018, 1, 6), CaseId = 1 },
            //    new Xray { Id = 2, Url = "teststomachxray", Date = new DateTime(2019, 9, 5), CaseId = 2 });

            modelBuilder.Entity<VetCase>()
                .HasData(
                new VetCase { VetId = 1, CaseId = 1 },
                new VetCase { VetId = 3, CaseId = 2 },
                new VetCase { VetId = 1, CaseId = 3 },
                new VetCase { VetId = 2, CaseId = 4 });

            modelBuilder.Entity<CasePetService>()
               .HasData(
               new CasePetService { CaseId = 1, PetServiceId = 2 },
               new CasePetService { CaseId = 2, PetServiceId = 1 },
               new CasePetService { CaseId = 3, PetServiceId = 2 },
               new CasePetService { CaseId = 4, PetServiceId = 3 });


            modelBuilder.Entity<Role>()
                .HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Vet" },
                new Role { Id = 3, Name = "Technician" });
        }
    }
}
