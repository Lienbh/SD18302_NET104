using App_Data_ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Configurations
{
    internal class SanPhamConfigs : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");
            builder.HasKey(p => p.id);
            builder.HasMany(p => p.GioHangCTs).WithOne(p => p.SanPham).HasForeignKey(p => p.ProductId);
            builder.HasMany(p => p.SanPhamCTs).WithOne(p => p.SanPham).HasForeignKey(p => p.ProductId);
            builder.HasMany(p => p.HoaDonCTs).WithOne(p => p.SanPham).HasForeignKey(p => p.ProductId);
        }
    }
}
