using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data
{
    public partial class WorldWideImporters : DbContext
    {
        public WorldWideImporters()
            : base("name=WorldWideImporters")
        {
            Database.SetInitializer<WorldWideImporters>(null);
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasMany(e => e.StateProvinces)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.LastEditedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Countries)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.LastEditedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.People1)
                .WithRequired(e => e.Person1)
                .HasForeignKey(e => e.LastEditedBy);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.StateProvinces)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.LastEditedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StateProvince>()
                .HasMany(e => e.Cities)
                .WithRequired(e => e.StateProvince)
                .WillCascadeOnDelete(false);
        }
    }
}
