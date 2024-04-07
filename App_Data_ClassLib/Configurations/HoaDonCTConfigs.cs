using App_Data_ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Configurations
{
    internal class HoaDonCTConfigs : IEntityTypeConfiguration<HoaDonCT>
    {
        public void Configure(EntityTypeBuilder<HoaDonCT> builder)
        {
            builder.ToTable("HoaDonCT");
            builder.HasKey(p => p.id);
            builder.HasOne(p => p.HoaDon).WithMany(p => p.HoaDonCTs).HasForeignKey(p => p.HoaDonId);
            builder.HasOne(p => p.SanPham).WithMany(p => p.HoaDonCTs).HasForeignKey(p => p.ProductId);
        }
    }
}
