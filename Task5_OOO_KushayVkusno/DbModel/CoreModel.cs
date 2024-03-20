using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task5_OOO_KushayVkusno.DbModel;

public partial class CoreModel : DbContext
{
    private static CoreModel instance;
    public static CoreModel init()
    {
        if(instance == null)
        {
            instance = new CoreModel();
        }
        return instance;
    }
    public CoreModel()
    {
    }

    public CoreModel(DbContextOptions<CoreModel> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cfif31.ru;port=3306;user id=ISPr23-35_ZlobinDS;password=ISPr23-35_ZlobinDS;database=ISPr23-35_ZlobinDS_Task5;character set=utf-8", ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.CategoryName).HasMaxLength(45);
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.IdDish).HasName("PRIMARY");

            entity.ToTable("Dish");

            entity.HasIndex(e => e.CategoryIdCategory, "fk_Dish_Category1_idx");

            entity.Property(e => e.IdDish).HasColumnName("idDish");
            entity.Property(e => e.CategoryIdCategory).HasColumnName("Category_idCategory");
            entity.Property(e => e.DishName).HasMaxLength(45);
            entity.Property(e => e.DishSostav).HasMaxLength(150);

            entity.HasOne(d => d.CategoryIdCategoryNavigation).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.CategoryIdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Dish_Category1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("Order");

            entity.HasIndex(e => e.DishIdDish, "fk_Order_Dish1_idx");

            entity.HasIndex(e => e.StatusIdStatus, "fk_Order_Status1_idx");

            entity.HasIndex(e => e.UserIdUser, "fk_Order_User1_idx");

            entity.Property(e => e.IdOrder).HasColumnName("idOrder");
            entity.Property(e => e.DishIdDish).HasColumnName("Dish_idDish");
            entity.Property(e => e.OrderDesc).HasMaxLength(45);
            entity.Property(e => e.OrderTime).HasMaxLength(45);
            entity.Property(e => e.StatusIdStatus).HasColumnName("Status_idStatus");
            entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

            entity.HasOne(d => d.DishIdDishNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DishIdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Dish1");

            entity.HasOne(d => d.StatusIdStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusIdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Status1");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_User1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.RoleName).HasMaxLength(45);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.StatusName).HasMaxLength(45);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.RoleIdRole, "fk_User_Role_idx");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.RoleIdRole).HasColumnName("Role_idRole");
            entity.Property(e => e.UserLogin).HasMaxLength(45);
            entity.Property(e => e.UserName).HasMaxLength(45);
            entity.Property(e => e.UserPassword).HasMaxLength(45);
            entity.Property(e => e.UserPatronymic).HasMaxLength(45);
            entity.Property(e => e.UserSurname).HasMaxLength(45);

            entity.HasOne(d => d.RoleIdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleIdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
