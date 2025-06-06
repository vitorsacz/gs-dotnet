using Microsoft.EntityFrameworkCore;
using AlertaCidadao.Domain.Entities;

namespace AlertaCidadao.Infraestructure.Data.AppData;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SafeResource> SafeResources { get; set; }
    public DbSet<ClimaticEvent> Events { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Phone)
                  .HasMaxLength(20);
            entity.Property(e => e.RegisterDate)
                  .IsRequired();
        });

        modelBuilder.Entity<ClimaticEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.Property(e => e.Description)
                  .HasMaxLength(500);
            entity.Property(e => e.EventTime)
                  .IsRequired();
            entity.Property(e => e.Latitude)
                  .HasColumnType("decimal(9,6)");
            entity.Property(e => e.Longitude)
                  .HasColumnType("decimal(9,6)");
            entity.Property(e => e.RiskLevel)
                  .IsRequired();
        });

        modelBuilder.Entity<SafeResource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Description)
                  .HasMaxLength(500);
            entity.Property(e => e.Latitude)
                  .HasColumnType("decimal(9,6)");
            entity.Property(e => e.Longitude)
                  .HasColumnType("decimal(9,6)");
            entity.Property(e => e.Capacity)
                  .IsRequired();
        });
    }

}
