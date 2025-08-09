using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class LblancasUsersPruebaTecnicaContext : DbContext
{
    public LblancasUsersPruebaTecnicaContext()
    {
    }

    public LblancasUsersPruebaTecnicaContext(DbContextOptions<LblancasUsersPruebaTecnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersGetDTO> UsersGetDTO { get; set; }

    public virtual DbSet<UsersGetByIdDTO> UsersGetByIdDTO { get; set; }
    public virtual DbSet<LoginDTO> LoginDTO { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.IdLocation).HasName("PK__Location__FB5FABA93910321A");

            entity.ToTable("Location");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.PlaceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("place_id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__C38F300931398742");

            entity.Property(e => e.IdPickUp).HasColumnName("IdPickUP");

            entity.HasOne(d => d.IdDropOffNavigation).WithMany(p => p.OrderIdDropOffNavigations)
                .HasForeignKey(d => d.IdDropOff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__IdDropOf__36B12243");

            entity.HasOne(d => d.IdPickUpNavigation).WithMany(p => p.OrderIdPickUpNavigations)
                .HasForeignKey(d => d.IdPickUp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__IdPickUP__35BCFE0A");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__IdStatus__34C8D9D1");

            entity.HasOne(d => d.IdTruckNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdTruck)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__IdTruck__33D4B598");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__IdUser__32E0915F");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C83CAA4AD");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__Status__B450643A37CFEB7F");

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.IdTruck).HasName("PK__Trucks__4E9972AAE43287BD");

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Plates)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__C781FF19520B5F91");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105340012EF7A").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__IdRol__2A4B4B5E");
        });

        modelBuilder.Entity<UsersGetDTO>(entity =>
            entity
                .HasNoKey());

        modelBuilder.Entity<UsersGetByIdDTO>(entity =>
            entity
                .HasNoKey());

        modelBuilder.Entity<LoginDTO>(entity =>
           entity
               .HasNoKey());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
