using ComputerComponentsApi.Models;
using Microsoft.EntityFrameworkCore;
namespace ComputerComponentsApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
        public DbSet<PC> PCs => Set<PC>();
        public DbSet<Component> Components => Set<Component>();
        public DbSet<ComponentType> ComponentTypes => Set<ComponentType>();
        public DbSet<ComponentManufacturer> ComponentManufacturers => Set<ComponentManufacturer>();
        public DbSet<PCComponent> PCComponents => Set<PCComponent>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PC>(entity =>
        {
                entity.ToTable("PCs");
                entity.HasKey(pc => pc.Id);
                entity.Property(pc => pc.Id)
                        .ValueGeneratedOnAdd();
                entity.Property(pc => pc.Name)
                        .HasMaxLength(50)
                        .IsRequired();
                entity.Property(pc => pc.Weight)
                        .IsRequired();
                entity.Property(pc => pc.Warranty)
                        .IsRequired();
                entity.Property(pc => pc.CreatedAt)
                        .HasColumnType("datetime")
                        .IsRequired();
                entity.Property(pc => pc.Stock)
                        .IsRequired();
        });
            modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
                entity.ToTable("ComponentManufacturers");
                entity.HasKey(manufacturer => manufacturer.Id);
                entity.Property(manufacturer => manufacturer.Id)
                        .ValueGeneratedOnAdd();
                entity.Property(manufacturer => manufacturer.Abbreviation)
                        .HasMaxLength(30)
                            .IsRequired();
                entity.Property(manufacturer => manufacturer.FullName)
                        .HasMaxLength(300)
                            .IsRequired();
                entity.Property(manufacturer => manufacturer.FoundationDate)
                        .HasColumnType("date")
                            .IsRequired();
        });
            modelBuilder.Entity<ComponentType>(entity =>
        {
                entity.ToTable("ComponentTypes");
                entity.HasKey(type => type.Id);
                entity.Property(type => type.Id)
                        .ValueGeneratedOnAdd();
                entity.Property(type => type.Abbreviation)
                        .HasMaxLength(30)
                            .IsRequired();
                entity.Property(type => type.Name)
                        .HasMaxLength(150)
                            .IsRequired();
        });
            modelBuilder.Entity<Component>(entity =>
        {
                entity.ToTable("Components");
                entity.HasKey(component => component.Code);
                entity.Property(component => component.Code)
                        .HasColumnType("char(10)")
                        .HasMaxLength(10)
                            .IsRequired();
                entity.Property(component => component.Name)
                        .HasMaxLength(300)
                            .IsRequired();
                entity.Property(component => component.Description)
                            .IsRequired();
                entity.Property(component => component.ComponentManufacturerId)
                        .HasColumnName("ComponentManufacturersId")
                            .IsRequired();
                entity.Property(component => component.ComponentTypeId)
                        .HasColumnName("ComponentTypesId")
                            .IsRequired();
                entity.HasOne(component => component.ComponentManufacturer)
                        .WithMany(manufacturer => manufacturer.Components)
                        .HasForeignKey(component => component.ComponentManufacturerId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(component => component.ComponentType)
                        .WithMany(type => type.Components)
                        .HasForeignKey(component => component.ComponentTypeId)
                        .OnDelete(DeleteBehavior.Restrict);
        });
            modelBuilder.Entity<PCComponent>(entity =>
        {
                entity.ToTable("PCComponents");
                entity.HasKey(pcComponent => new
            {
                    pcComponent.PCId,
                    pcComponent.ComponentCode
            });
                entity.Property(pcComponent => pcComponent.ComponentCode)
                        .HasColumnType("char(10)")
                        .HasMaxLength(10)
                            .IsRequired();
                entity.Property(pcComponent => pcComponent.Amount)
                            .IsRequired();
                entity.HasOne(pcComponent => pcComponent.PC)
                        .WithMany(pc => pc.PCComponents)
                        .HasForeignKey(pcComponent => pcComponent.PCId)
                        .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(pcComponent => pcComponent.Component)
                        .WithMany(component => component.PCComponents)
                        .HasForeignKey(pcComponent => pcComponent.ComponentCode)
                        .OnDelete(DeleteBehavior.Restrict);
        });
    }
}