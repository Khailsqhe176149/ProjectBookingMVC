using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectBookingMVC.Models;

public partial class BookingHotelContext : DbContext
{
    public BookingHotelContext()
    {
    }

    public BookingHotelContext(DbContextOptions<BookingHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DB"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CheckIn)
                .HasColumnType("date")
                .HasColumnName("check_in");
            entity.Property(e => e.CheckOut)
                .HasColumnType("date")
                .HasColumnName("check_out");
            entity.Property(e => e.IdRoom).HasColumnName("id_room");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bed).HasColumnName("bed");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Services).HasColumnName("services");
            entity.Property(e => e.Size).HasColumnName("size");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_1");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
