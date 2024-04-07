namespace App_MVC.Models
{
    public class GioHangCTDTO
    {
        public Guid Id { get; set; }
        public Guid GioHangId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}
