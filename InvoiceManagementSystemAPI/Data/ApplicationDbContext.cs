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
        
    }
}
