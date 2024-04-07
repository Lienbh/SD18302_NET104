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
    internal class ThanhToanConfigs : IEntityTypeConfiguration<ThanhToan>
    {
        public void Configure(EntityTypeBuilder<ThanhToan> builder)
        {
            builder.ToTable("ThanhToan");
            builder.HasKey(p => p.Id);
             builder.HasOne(p => p.User).WithMany(p => p.ThanhToans).HasForeignKey(p => p.UserId);

        }
    }
}
