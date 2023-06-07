using Assignment.DataAccess.ValueConverters;
using Assignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DataAccess;

public class ReportDbContext : DbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reports> Reports { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reports>(entity =>
        {
            entity.Property(e => e.ReportStatus)
                .HasComment("Is this report ready?");

            entity.Property(e => e.RequestedAt)
                .HasComment("When this report is requested");

            entity.Property(e => e.ReportContents)
                .HasComment("Contents of the report")
                .HasConversion(new DictionaryConverter())
                .HasColumnType("jsonb");
        });
    }
}
