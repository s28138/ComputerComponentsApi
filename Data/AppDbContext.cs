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
                SeedData(modelBuilder);
    }
    private static void SeedData(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<ComponentManufacturer>().HasData(
                new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateOnly(1969, 5, 1)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateOnly(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateOnly(1994, 1, 1)
            }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ComponentManufacturerId = 1,
                ComponentTypeId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ComponentManufacturerId = 2,
                ComponentTypeId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ComponentManufacturerId = 3,
                ComponentTypeId = 3
            }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new PC
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new PC
            {
                Id = 3,
                Name = "Creator Station",
                Weight = 9.8,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0),
                Stock = 3
            }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "GPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 1,
                ComponentCode = "RAM0000001",
                Amount = 2
            },
            new PCComponent
            {
                PCId = 2,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 2,
                ComponentCode = "RAM0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 3,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 3,
                ComponentCode = "GPU0000001",
                Amount = 1
            },
            new PCComponent
            {
                PCId = 3,
                ComponentCode = "RAM0000001",
                Amount = 4
            }
        );
    }
}

