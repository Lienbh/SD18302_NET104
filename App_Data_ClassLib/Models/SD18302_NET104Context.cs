﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App_Data_ClassLib.Models
{
    public partial class SD18302_NET104Context : DbContext
    {
        public SD18302_NET104Context()
        {
        }

        public SD18302_NET104Context(DbContextOptions<SD18302_NET104Context> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<GioHangCT> gioHangCTs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-A9Q63JRK\\SQLEXPRESS;Database=SD18302_NET104;Trusted_Connection=True;TrustServerCertificate=True\n");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
