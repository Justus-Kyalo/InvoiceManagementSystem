using InvoiceManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace InvoiceManagementSystemAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Invoice> Invoices { get; set; }
        
    }
}
