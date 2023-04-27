﻿// <auto-generated />
using System;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eshop_Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230423161552_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Eshop_Domain.Entities.ProductEntities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("UserCartId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("UserCartId");

                    b.HasIndex("UserId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.ProductEntities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.RefreshTokenUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserRefreshTokenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "UserRefreshTokenId");

                    b.HasIndex("UserRefreshTokenId");

                    b.ToTable("RefreshTokenUser");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("LockOutEnable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LockOutEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUsername")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserHash")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserCart");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserOrders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrders");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserRefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshTokenValue")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("UserRefreshToken");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 23, 12, 15, 52, 394, DateTimeKind.Local).AddTicks(1659),
                            ModifyAt = new DateTime(2023, 4, 23, 12, 15, 52, 394, DateTimeKind.Local).AddTicks(1668),
                            Name = "Customer",
                            NormalizedName = "Customer",
                            UserId = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 23, 12, 15, 52, 394, DateTimeKind.Local).AddTicks(1674),
                            ModifyAt = new DateTime(2023, 4, 23, 12, 15, 52, 394, DateTimeKind.Local).AddTicks(1674),
                            Name = "Administrator",
                            NormalizedName = "Administrator",
                            UserId = 0
                        });
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserSalt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SaltValue")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSalt");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserShippingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserShippingInfo");
                });

            modelBuilder.Entity("UserUserRoles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserRolesId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "UserRolesId");

                    b.HasIndex("UserRolesId");

                    b.ToTable("UserUserRoles");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.ProductEntities.Product", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.ProductEntities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eshop_Domain.Entities.UserEntities.UserCart", null)
                        .WithMany("Product")
                        .HasForeignKey("UserCartId");

                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.RefreshTokenUser", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("RefreshTokenUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eshop_Domain.Entities.UserEntities.UserRefreshToken", "UserRefreshToken")
                        .WithMany("RefreshTokenUser")
                        .HasForeignKey("UserRefreshTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserRefreshToken");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserCart", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserCart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserOrders", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserSalt", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserSalts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserShippingInfo", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", "User")
                        .WithMany("UserShippingInfos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserUserRoles", b =>
                {
                    b.HasOne("Eshop_Domain.Entities.UserEntities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eshop_Domain.Entities.UserEntities.UserRoles", null)
                        .WithMany()
                        .HasForeignKey("UserRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eshop_Domain.Entities.ProductEntities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.User", b =>
                {
                    b.Navigation("RefreshTokenUsers");

                    b.Navigation("UserCart");

                    b.Navigation("UserOrders");

                    b.Navigation("UserProducts");

                    b.Navigation("UserSalts");

                    b.Navigation("UserShippingInfos");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserCart", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Eshop_Domain.Entities.UserEntities.UserRefreshToken", b =>
                {
                    b.Navigation("RefreshTokenUser");
                });
#pragma warning restore 612, 618
        }
    }
}
