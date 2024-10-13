﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BrosShop.Models;

public partial class BrosShopDbContext : DbContext
{
    public BrosShopDbContext()
    {
    }

    public BrosShopDbContext(DbContextOptions<BrosShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BrosShopAttribute> BrosShopAttributes { get; set; }

    public virtual DbSet<BrosShopCategory> BrosShopCategories { get; set; }

    public virtual DbSet<BrosShopOrder> BrosShopOrders { get; set; }

    public virtual DbSet<BrosShopOrderComposition> BrosShopOrderCompositions { get; set; }

    public virtual DbSet<BrosShopProduct> BrosShopProducts { get; set; }

    public virtual DbSet<BrosShopProductAttribute> BrosShopProductAttributes { get; set; }

    public virtual DbSet<BrosShopReview> BrosShopReviews { get; set; }

    public virtual DbSet<BrosShopUser> BrosShopUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=166.1.201.241;database=BrosShopDB;user id=BrosShopAdm;password=BrosShopAdmin", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<BrosShopAttribute>(entity =>
        {
            entity.HasKey(e => e.BrosShopAttributesId).HasName("PRIMARY");

            entity.ToTable("BrosShop_Attributes");

            entity.Property(e => e.BrosShopAttributesId).HasColumnName("BrosShop_AttributesId");
            entity.Property(e => e.BrosShopColor)
                .HasMaxLength(20)
                .HasColumnName("BrosShop_Color");
            entity.Property(e => e.BrosShopSize)
                .HasMaxLength(10)
                .HasColumnName("BrosShop_Size");
        });

        modelBuilder.Entity<BrosShopCategory>(entity =>
        {
            entity.HasKey(e => e.BrosShopCategoryId).HasName("PRIMARY");

            entity.ToTable("BrosShop_Category");

            entity.Property(e => e.BrosShopCategoryId).HasColumnName("BrosShop_CategoryId");
            entity.Property(e => e.BrosShopCategoryTitle)
                .HasMaxLength(45)
                .HasColumnName("BrosShop_CategoryTitle");
        });

        modelBuilder.Entity<BrosShopOrder>(entity =>
        {
            entity.HasKey(e => e.BrosShopOrderId).HasName("PRIMARY");

            entity.ToTable("BrosShop_Order");

            entity.Property(e => e.BrosShopOrderId).HasColumnName("BrosShop_OrderId");
            entity.Property(e => e.BrosShopDateTimeOrder)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("BrosShop_DateTimeOrder");
            entity.Property(e => e.BrosShopTypeOrder)
                .HasDefaultValueSql("'касса'")
                .HasColumnType("enum('веб-сайт','касса','WB')")
                .HasColumnName("BrosShop_TypeOrder");
            entity.Property(e => e.BrosShopUserId).HasColumnName("BrosShop_UserId");
        });

        modelBuilder.Entity<BrosShopOrderComposition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BrosShop_OrderComposition");

            entity.HasIndex(e => e.BrosShopOrderId, "BrosShop_OrderComposition_ibfk_2");

            entity.HasIndex(e => new { e.BrosShopProductId, e.BrosShopOrderId }, "BrosShop_ProductId").IsUnique();

            entity.Property(e => e.BrosShopOrderId).HasColumnName("BrosShop_OrderId");
            entity.Property(e => e.BrosShopProductId).HasColumnName("BrosShop_ProductId");
            entity.Property(e => e.BrosShopQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnName("BrosShop_Quantity");

            entity.HasOne(d => d.BrosShopOrder).WithMany()
                .HasForeignKey(d => d.BrosShopOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_OrderComposition_ibfk_2");

            entity.HasOne(d => d.BrosShopProduct).WithMany()
                .HasForeignKey(d => d.BrosShopProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_OrderComposition_ibfk_1");
        });

        modelBuilder.Entity<BrosShopProduct>(entity =>
        {
            entity.HasKey(e => e.BrosShopProductId).HasName("PRIMARY");

            entity.ToTable("BrosShop_Product");

            entity.HasIndex(e => e.BrosShopCategoryId, "BrosShop_FK_Category_idx");

            entity.Property(e => e.BrosShopProductId).HasColumnName("BrosShop_ProductId");
            entity.Property(e => e.BrosShopCategoryId).HasColumnName("BrosShop_CategoryId");
            entity.Property(e => e.BrosShopDescription)
                .HasMaxLength(500)
                .HasColumnName("BrosShop_Description");
            entity.Property(e => e.BrosShopDiscountPercent)
                .HasDefaultValueSql("'0'")
                .HasColumnName("BrosShop_DiscountPercent");
            entity.Property(e => e.BrosShopPrice)
                .HasPrecision(6, 2)
                .HasColumnName("BrosShop_Price");
            entity.Property(e => e.BrosShopTitle)
                .HasMaxLength(100)
                .HasColumnName("BrosShop_Title");
            entity.Property(e => e.BrosShopWbarticul).HasColumnName("BrosShop_WBArticul");

            entity.HasOne(d => d.BrosShopCategory).WithMany(p => p.BrosShopProducts)
                .HasForeignKey(d => d.BrosShopCategoryId)
                .HasConstraintName("BrosShop_FK_Category");
        });

        modelBuilder.Entity<BrosShopProductAttribute>(entity =>
        {
            entity.HasKey(e => new { e.BrosShopProductId, e.BrosShopAttributesId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("BrosShop_Product_Attributes");

            entity.HasIndex(e => e.BrosShopAttributesId, "BrosShop_Product_Attributes_ibfk_2");

            entity.Property(e => e.BrosShopProductId).HasColumnName("BrosShop_ProductId");
            entity.Property(e => e.BrosShopAttributesId).HasColumnName("BrosShop_AttributesId");
            entity.Property(e => e.BrosShopCount)
                .HasDefaultValueSql("'1'")
                .HasColumnName("BrosShop_Count");

            entity.HasOne(d => d.BrosShopAttributes).WithMany(p => p.BrosShopProductAttributes)
                .HasForeignKey(d => d.BrosShopAttributesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_Product_Attributes_ibfk_2");

            entity.HasOne(d => d.BrosShopProduct).WithMany(p => p.BrosShopProductAttributes)
                .HasForeignKey(d => d.BrosShopProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_Product_Attributes_ibfk_1");
        });

        modelBuilder.Entity<BrosShopReview>(entity =>
        {
            entity.HasKey(e => e.BrosShopReviewId).HasName("PRIMARY");

            entity.ToTable("BrosShop_Review");

            entity.HasIndex(e => e.BrosShopProductId, "BrosShop_Review_ibfk_1");

            entity.HasIndex(e => e.BrosShopUserId, "BrosShop_Review_ibfk_2");

            entity.Property(e => e.BrosShopReviewId).HasColumnName("BrosShop_ReviewId");
            entity.Property(e => e.BrosShopComment)
                .HasColumnType("text")
                .HasColumnName("BrosShop_Comment");
            entity.Property(e => e.BrosShopDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("BrosShop_DateTime");
            entity.Property(e => e.BrosShopProductId).HasColumnName("BrosShop_ProductId");
            entity.Property(e => e.BrosShopRating).HasColumnName("BrosShop_Rating");
            entity.Property(e => e.BrosShopUserId).HasColumnName("BrosShop_UserId");

            entity.HasOne(d => d.BrosShopProduct).WithMany(p => p.BrosShopReviews)
                .HasForeignKey(d => d.BrosShopProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_Review_ibfk_1");

            entity.HasOne(d => d.BrosShopUser).WithMany(p => p.BrosShopReviews)
                .HasForeignKey(d => d.BrosShopUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrosShop_Review_ibfk_2");
        });

        modelBuilder.Entity<BrosShopUser>(entity =>
        {
            entity.HasKey(e => e.BrosShopUserId).HasName("PRIMARY");

            entity.ToTable("BrosShop_User");

            entity.Property(e => e.BrosShopUserId).HasColumnName("BrosShop_UserId");
            entity.Property(e => e.BrosShopEmail)
                .HasMaxLength(100)
                .HasColumnName("BrosShop_Email");
            entity.Property(e => e.BrosShopFullName)
                .HasMaxLength(100)
                .HasColumnName("BrosShop_FullName");
            entity.Property(e => e.BrosShopPassword)
                .HasMaxLength(255)
                .HasColumnName("BrosShop_Password");
            entity.Property(e => e.BrosShopRegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("BrosShop_RegistrationDate");
            entity.Property(e => e.BrosShopUsername)
                .HasMaxLength(50)
                .HasColumnName("BrosShop_Username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
