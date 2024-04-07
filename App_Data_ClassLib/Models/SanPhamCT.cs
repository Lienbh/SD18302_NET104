using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class SanPhamCT
    {
        public Guid id { get; set; }
        public Guid ProductId { get; set; }
        public string NPP { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual NPP Npp { get; set; }

    }
}
