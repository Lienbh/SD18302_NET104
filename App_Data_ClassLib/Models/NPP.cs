using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class NPP
    {
        public Guid id { get; set; }   
        public string TenNPP { get; set; }
        public virtual SanPhamCT SanPhamCT { get; set; }
    }
}
