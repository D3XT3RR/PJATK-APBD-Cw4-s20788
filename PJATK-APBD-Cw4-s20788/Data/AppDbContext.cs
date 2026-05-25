using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s20788.Models;

namespace PJATK_APBD_Cw4_s20788.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Pc> Pcs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.ToTable("PCs");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Weight)
                .HasColumnType("float")
                .IsRequired();

            entity.Property(e => e.Warranty)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.Stock)
                .IsRequired();
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.ToTable("ComponentTypes");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.ToTable("ComponentManufacturers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.FoundationDate)
                .HasColumnType("date")
                .IsRequired();
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.ToTable("Components");

            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entity.Property(e => e.ComponentManufacturerId)
                .HasColumnName("ComponentManufacturersId")
                .IsRequired();

            entity.Property(e => e.ComponentTypeId)
                .HasColumnName("ComponentTypesId")
                .IsRequired();

            entity.HasOne(e => e.ComponentManufacturer)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ComponentType)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.ToTable("PCComponents");

            entity.HasKey(e => new { e.PcId, e.ComponentCode });

            entity.Property(e => e.PcId)
                .HasColumnName("PCId")
                .IsRequired();

            entity.Property(e => e.ComponentCode)
                .HasColumnType("char(10)")
                .IsRequired();

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.HasOne(e => e.Pc)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "Workstation Ultra",
                Weight = 15.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0),
                Stock = 3
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

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer
            {
                Id = 1,
                Abbreviation = "INTEL",
                FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            },
            new ComponentManufacturer
            {
                Id = 2,
                Abbreviation = "NVIDIA",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateTime(1993, 4, 5)
            },
            new ComponentManufacturer
            {
                Id = 3,
                Abbreviation = "KINGSTON",
                FullName = "Kingston Technology",
                FoundationDate = new DateTime(1987, 10, 17)
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU001",
                Name = "Intel Core i7-14700K",
                Description = "High performance desktop processor",
                ComponentManufacturerId = 1,
                ComponentTypeId = 1
            },
            new Component
            {
                Code = "GPU001",
                Name = "NVIDIA GeForce RTX 4070",
                Description = "Graphics card for gaming and productivity",
                ComponentManufacturerId = 2,
                ComponentTypeId = 2
            },
            new Component
            {
                Code = "RAM001",
                Name = "Kingston Fury 32GB DDR5",
                Description = "DDR5 memory kit",
                ComponentManufacturerId = 3,
                ComponentTypeId = 3
            }
        );

        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "CPU001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "GPU001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "RAM001",
                Amount = 2
            },
            new PcComponent
            {
                PcId = 2,
                ComponentCode = "CPU001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 2,
                ComponentCode = "RAM001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 3,
                ComponentCode = "CPU001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 3,
                ComponentCode = "GPU001",
                Amount = 2
            }
        );
    }
}