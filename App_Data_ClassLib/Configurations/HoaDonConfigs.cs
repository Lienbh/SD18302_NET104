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
    internal class HoaDonConfigs : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {

            builder.HasKey(x => x.Id);
            //Config khóa ngoại 
            builder.HasOne(p => p.User).WithMany(p => p.HoaDons).HasForeignKey(p => p.UserId);
           // builder.HasOne(p => p.User).WithOne(p => p.GioHang).HasForeignKey<User>(p => p.ID);
            //Với mỗi User sẽ có nhiều Hóa đơn, khóa ngoại là cột UserID nối với bảng Userd
            //Tên của khóa ngoại là FK_User_HD


        }
    }
}
