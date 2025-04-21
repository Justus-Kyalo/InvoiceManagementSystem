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
        
        public DbSet<IIFBackup> IIFBackups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Slip->SlipItem ,Has one to many
            modelBuilder.Entity<Slip>()
                .HasMany(s => s.SlipItems)
                .WithOne(si => si.Slip)
                .HasForeignKey(si => si.SlipId)
                .OnDelete(DeleteBehavior.Cascade);
            
            //Item->SlipItem ,Has one to many
            modelBuilder.Entity<Item>()
                .HasMany(s => s.SlipItems)
                .WithOne(si => si.Item)
                .HasForeignKey(si => si.ItemId);
            
            //unique constraint
            modelBuilder.Entity<Item>()
                .HasIndex(i => i.ItemName)
                .IsUnique();

            modelBuilder.Entity<Slip>()
                .HasIndex(i => i.SlipNumber)
                .IsUnique();


        }

        
    }
}
