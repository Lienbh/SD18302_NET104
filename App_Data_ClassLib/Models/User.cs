using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DoB { get; set; }

        //ICollection<HoaDon> HoaDons chỉ để thể hiện 1 user có thể có nhiều Hóa đơn
        //ICollection<HoaDon> Còn được sử dụng để làm Navigation để trỏ đến khi cần 
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<GioHang> GioHang { get; set; }
        public virtual ICollection<ThanhToan> ThanhToans { get; set; }
    }
}
