using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class ThanhToan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int loaiTT { get; set; }
        public int soThe { get; set; }
        public virtual User User { get; set; }

    }
}
