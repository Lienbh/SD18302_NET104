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
    internal class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Thực hiện các config trên entity
            builder.ToTable("User"); //Đặt tên bảng
                                      //Xác định khóa chính
          // builder.HasNoKey(); //Không khóa
            builder.HasKey(p => p.ID); //Set khóa là cột Id
            //builder.HasKey(p => new { p.ID, p.Address }); //Khóa chính Composite (Nhiều cột)
            //Thiết lập thuộc tính cho cột
            builder.Property(p => p.Name).HasColumnName("Ten").HasColumnType("nvarchar(50)");
            builder.Property(p => p.Address).HasColumnName("DiaChi").HasMaxLength(50).IsFixedLength().IsUnicode(true); //nvarchar(50)
            builder.Property(p => p.Username).IsRequired(); //Not null
        }
    }
}
