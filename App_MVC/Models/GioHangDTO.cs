using System.Collections;

namespace App_MVC.Models;

public class GioHangDTO
{
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Decimal? TotalMoney { get; set; } 
    public string FullName { get; set; } 
    public string Email { get; set; } 
    public int Status { get; set; } 
    public string Address { get; set; } 
    public string PhoneNumber { get; set; } 
    public IEnumerable<GioHangCTDTO> GioHangChiTiet { get; set; }
}