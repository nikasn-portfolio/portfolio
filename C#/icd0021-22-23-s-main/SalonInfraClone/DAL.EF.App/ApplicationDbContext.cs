using Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Category> Categories { get; set; } = default!;
    
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; }  = default!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
    //public DbSet<UserRecord> UserRecords { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<InvoiceRow> InvoiceRows { get; set; } = default!;
    public DbSet<InvoiceFooter> InvoiceFooters { get; set; } = default!;
    public DbSet<Record> Records { get; set; } = default!;
    public DbSet<Language> Languages { get; set; } = default!;
    
    public DbSet<RecordService> RecordServices { get; set; } = default!;
    public DbSet<Client> Clients { get; set; } = default!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }
    
    
}
