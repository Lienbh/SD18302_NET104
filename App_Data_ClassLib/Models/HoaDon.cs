﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Models
{
    public class HoaDon
    {
        public Guid Id { get; set; }
        public DateTime SoldDate { get; set; }
        public Guid UserId { get; set; }
   
        public Decimal TotalMoney { get; set; } 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public string Address { get; set; } 
        public string PhoneNumber { get; set; } 
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<HoaDonCT> HoaDonCTs { get; set; }
    }
}
