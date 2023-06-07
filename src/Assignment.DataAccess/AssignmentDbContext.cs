using Assignment.DataAccess.ValueConverters;
using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DataAccess;

public class AssignmentDbContext : DbContext
{
    public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contacts> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contacts>(entity =>
        {
            entity.Property(e => e.Company)
                .HasComment("Where this contact works");

            entity.Property(e => e.Name)
                .HasComment("Name of the contact");
            entity.Property(e => e.Surname)
                .HasComment("Surname of the contact");
            
            entity.Property(e => e.ContactInfo)
                .HasComment("List of connection info of the contact")
                .HasConversion(new ContactInfoArrayConverter())
                .HasColumnType("jsonb");
        });
    }
}