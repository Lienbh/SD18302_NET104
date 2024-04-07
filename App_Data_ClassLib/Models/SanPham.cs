using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class SanPham
    {
        public Guid id { get; set; }
        public string ProductName { get; set; }
        public int quantity { get; set; }
        public Decimal price { get; set; }
        public string ImgURL { get; set; }
        public int status { get; set; }
       // public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<GioHangCT> GioHangCTs { get; set; }
        public virtual ICollection<HoaDonCT> HoaDonCTs { get; set; }
        public virtual ICollection<SanPhamCT> SanPhamCTs { get; set; }


    }
}
