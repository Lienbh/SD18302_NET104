using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class KhuyenMai
    {
        public Guid Id { get; set; }
        public string KMName { get; set; }
        public int ChietKhau { get; set; }
        public virtual HoaDonCT HoaDonCT { get; set; }
    }
}
