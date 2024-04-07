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
    internal class SanPhamCTConfigs : IEntityTypeConfiguration<SanPhamCT>
    {
        public void Configure(EntityTypeBuilder<SanPhamCT> builder)
        {
            builder.ToTable("SanPhamCT");
            builder.HasKey(p => p.id);
            builder.HasOne(p => p.SanPham).WithMany(p => p.SanPhamCTs).HasForeignKey(p => p.ProductId);

        }
    }
}
