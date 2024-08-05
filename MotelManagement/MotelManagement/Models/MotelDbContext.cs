using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MotelManagement.Models;

public partial class MotelDbContext : DbContext
{
    public MotelDbContext()
    {
    }

    public MotelDbContext(DbContextOptions<MotelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Renter> Renters { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomHistory> RoomHistories { get; set; }

    public virtual DbSet<RoomItem> RoomItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-O9UE30VC;uid=sa;pwd=sa;database=Motel_DB;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Account");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.UserMail)
                .HasMaxLength(50)
                .HasColumnName("userMail");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .HasColumnName("userPassword");
            entity.Property(e => e.UserRole).HasColumnName("userRole");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("itemID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("itemName");
        });

        modelBuilder.Entity<Renter>(entity =>
        {
            entity.ToTable("Renter");

            entity.Property(e => e.RenterId)
                .ValueGeneratedNever()
                .HasColumnName("renterID");
            entity.Property(e => e.RenterHaveRoom).HasColumnName("renterHaveRoom");
            entity.Property(e => e.RenterStatus).HasColumnName("renterStatus");
            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.UserId).HasColumnName("userID");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("request");

            entity.Property(e => e.CreateAt)
                .HasMaxLength(50)
                .HasColumnName("createAt");
            entity.Property(e => e.Desciption)
                .HasMaxLength(50)
                .HasColumnName("desciption");
            entity.Property(e => e.RequesstId).HasColumnName("requesstId");
            entity.Property(e => e.RequestType).HasColumnName("requestType");
            entity.Property(e => e.ResStatus)
                .HasMaxLength(50)
                .HasColumnName("resStatus");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("requestType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RequestName)
                .HasMaxLength(50)
                .HasColumnName("requestName");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("roomID");
            entity.Property(e => e.RoomFee)
                .HasColumnType("money")
                .HasColumnName("roomFee");
            entity.Property(e => e.RoomFloor).HasColumnName("roomFloor");
            entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");
            entity.Property(e => e.RoomSize).HasColumnName("roomSize");
            entity.Property(e => e.RoomStatus).HasColumnName("roomStatus");
        });

        modelBuilder.Entity<RoomHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId);

            entity.ToTable("RoomHistory");

            entity.Property(e => e.HistoryId).HasColumnName("historyID");
            entity.Property(e => e.CheckIn).HasColumnName("checkIn");
            entity.Property(e => e.CheckOut).HasColumnName("checkOut");
            entity.Property(e => e.CreateAt).HasColumnName("createAt");
            entity.Property(e => e.RenterId).HasColumnName("renterID");
            entity.Property(e => e.RoomId).HasColumnName("roomID");
            entity.Property(e => e.UserId).HasColumnName("userID");
        });

        modelBuilder.Entity<RoomItem>(entity =>
        {
            entity.ToTable("RoomItem");

            entity.Property(e => e.RoomItemId)
                .ValueGeneratedNever()
                .HasColumnName("roomItemId");
            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RoomId).HasColumnName("roomID");

            entity.HasOne(d => d.Item).WithMany(p => p.RoomItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomItem_Item");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomItems)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomItem_Room");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.UserAddress).HasMaxLength(50);
            entity.Property(e => e.UserGender).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPhone).HasMaxLength(20);

            entity.HasOne(d => d.UserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
