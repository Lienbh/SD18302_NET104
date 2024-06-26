﻿// <auto-generated />
using System;
using App_Data_ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App_Data_ClassLib.Migrations
{
    [DbContext(typeof(SD18302_NET104Context))]
    [Migration("20240405165036_initdb")]
    partial class initdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App_Data_ClassLib.Models.GioHang", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GioHang", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.GioHangCT", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GioHangId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("GioHangId");

                    b.HasIndex("ProductId");

                    b.ToTable("GioHangCT", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.HoaDon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SoldDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.HoaDonCT", b =>
                {
                    b.Property<Guid>("id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("SellAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SellPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("ProductId");

                    b.ToTable("HoaDonCT", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.KhuyenMai", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChietKhau")
                        .HasColumnType("int");

                    b.Property<string>("KMName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KhuyenMai", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.NPP", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TenNPP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("NPP", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.SanPham", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImgURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.SanPhamCT", b =>
                {
                    b.Property<Guid>("id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NPP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("ProductId");

                    b.ToTable("SanPhamCT", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.ThanhToan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("loaiTT")
                        .HasColumnType("int");

                    b.Property<int>("soThe")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ThanhToan", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("DiaChi")
                        .IsFixedLength();

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Ten");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.GioHangCT", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.GioHang", "GioHang")
                        .WithMany("GioHangCTs")
                        .HasForeignKey("GioHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data_ClassLib.Models.SanPham", "SanPham")
                        .WithMany("GioHangCTs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.HoaDon", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.User", "User")
                        .WithMany("HoaDons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.HoaDonCT", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.HoaDon", "HoaDon")
                        .WithMany("HoaDonCTs")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data_ClassLib.Models.SanPham", "SanPham")
                        .WithMany("HoaDonCTs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data_ClassLib.Models.KhuyenMai", "KhuyenMai")
                        .WithOne("HoaDonCT")
                        .HasForeignKey("App_Data_ClassLib.Models.HoaDonCT", "id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("KhuyenMai");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.SanPhamCT", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.SanPham", "SanPham")
                        .WithMany("SanPhamCTs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_Data_ClassLib.Models.NPP", "Npp")
                        .WithOne("SanPhamCT")
                        .HasForeignKey("App_Data_ClassLib.Models.SanPhamCT", "id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Npp");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.ThanhToan", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.User", "User")
                        .WithMany("ThanhToans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.User", b =>
                {
                    b.HasOne("App_Data_ClassLib.Models.GioHang", "GioHang")
                        .WithOne("User")
                        .HasForeignKey("App_Data_ClassLib.Models.User", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GioHang");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.GioHang", b =>
                {
                    b.Navigation("GioHangCTs");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.HoaDon", b =>
                {
                    b.Navigation("HoaDonCTs");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.KhuyenMai", b =>
                {
                    b.Navigation("HoaDonCT")
                        .IsRequired();
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.NPP", b =>
                {
                    b.Navigation("SanPhamCT")
                        .IsRequired();
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.SanPham", b =>
                {
                    b.Navigation("GioHangCTs");

                    b.Navigation("HoaDonCTs");

                    b.Navigation("SanPhamCTs");
                });

            modelBuilder.Entity("App_Data_ClassLib.Models.User", b =>
                {
                    b.Navigation("HoaDons");

                    b.Navigation("ThanhToans");
                });
#pragma warning restore 612, 618
        }
    }
}
