using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class HoaDonCT
    {
        public Guid id { get; set; }
        public Guid HoaDonId { get; set; }
        public Guid ProductId { get; set; }
        public Decimal SellPrice { get; set; }
        public decimal SellAmount { get; set; }
        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual  KhuyenMai KhuyenMai { get; set; }



    }
}
