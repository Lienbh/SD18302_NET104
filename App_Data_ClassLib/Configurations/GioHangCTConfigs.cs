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
    internal class GioHangCTConfigs : IEntityTypeConfiguration<GioHangCT>
    {
        public void Configure(EntityTypeBuilder<GioHangCT> builder)
        {
            builder.ToTable("GioHangCT");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.GioHang).WithMany(p => p.GioHangCTs).HasForeignKey(p => p.GioHangId);
            builder.HasOne(p => p.SanPham).WithMany(p => p.GioHangCTs).HasForeignKey(p => p.GioHangId);
        }
    }
}
