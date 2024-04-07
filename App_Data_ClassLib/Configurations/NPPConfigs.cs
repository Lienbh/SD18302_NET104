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
    internal class NPPConfigs : IEntityTypeConfiguration<NPP>
    {
        public void Configure(EntityTypeBuilder<NPP> builder)
        {
            builder.ToTable("NPP");
            builder.HasKey(p => p.id);
            builder.HasOne(p => p.SanPhamCT).WithOne(p => p.Npp).HasForeignKey<SanPhamCT>(p => p.id);

        }
    }
}
