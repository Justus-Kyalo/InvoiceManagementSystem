using InvoiceManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace InvoiceManagementSystemAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Slip> Slips { get; set; }
        public DbSet<SlipItem> SlipItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Item> Items { get; set; }
        
        public DbSet<CustomerItemPrice> CustomerItemPrices { get; set; }
        
        public DbSet<IIFBackup> IIFBackups { get; set; }
        
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        
        public DbSet<SlipDetail> SlipDetails { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Slip->SlipItem ,Has one to many
            modelBuilder.Entity<Slip>()
                .HasMany(s => s.SlipItems)
                .WithOne(si => si.Slip)
                .HasForeignKey(si => si.SlipId)
                .OnDelete(DeleteBehavior.Cascade);
            // Customer->CustomerItemPrice ,Has one to many
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustomerItemPrices)
                .WithOne(cip => cip.Customer)
                .HasForeignKey(cip => cip.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Item->SlipItem ,Has one to many
            modelBuilder.Entity<Item>()
                .HasMany(s => s.SlipItems)
                .WithOne(si => si.Item)
                .HasForeignKey(si => si.ItemId);
            
            // unique constraint
            modelBuilder.Entity<Item>()
                .HasIndex(i => i.ItemName)
                .IsUnique();
        
            modelBuilder.Entity<Slip>()
                .HasIndex(i => i.SlipNumber)
                .IsUnique();
            modelBuilder.Entity<SlipDetail>()
                .ToView("vw_SlipDetails")
                .HasKey(vw => vw.SlipId);


        }

        
    }
}
