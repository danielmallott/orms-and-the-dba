using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Data
{
    public partial class WideWorldImporters : DbContext
    {
        public WideWorldImporters()
            : base("name=WideWorldImporters")
        {
            Database.SetInitializer<WideWorldImporters>(null);
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.StandardDiscountPercentage)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Customers1)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.BillToCustomerID);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.BillToCustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Invoices1)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.CustomerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.TaxRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.InvoiceLines)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderLine>()
                .Property(e => e.TaxRate)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderLines)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Orders1)
                .WithOptional(e => e.Order1)
                .HasForeignKey(e => e.BackorderOrderID);
        }
    }
}
