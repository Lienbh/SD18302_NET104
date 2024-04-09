using App_Data_ClassLib.Models;
using App_Data_ClassLib.Repository;
using App_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace App_MVC.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHangController
        AllRepository<SanPham> _reps;
        AllRepository<GioHang> _repGioHang;
        AllRepository<GioHangCT> _repGioHangCT;
        SD18302_NET104Context _context;
        DbSet<SanPham> _sp;
        DbSet<GioHang> _gh;
        DbSet<GioHangCT> _ghct;
        private bool AnBtnThanhToan = false;
   

        public GioHangController()
        {
            _context = new SD18302_NET104Context();
            _sp = _context.SanPhams;
            _reps = new AllRepository<SanPham>(_sp, _context);
            _repGioHang = new AllRepository<GioHang>(_gh, _context);
            _repGioHangCT = new AllRepository<GioHangCT>(_ghct, _context);
            
        }
        public ActionResult Index()
        {
            ViewData["TongTien"] = 0;
            ViewData["CheckSoLuong"] = null;
            ViewData["AnBtnThanhToan"] = "false";
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");
            decimal tongTien = 0;
            //gioHangCTDTO là bao gồm các thông tin của 1 giỏ hàng bình
            //thường và thêm thêm 1 trường là tên của sản phẩm để khi show lên phần
            //giỏ hàng sẽ có tên của sản phẩm
            List<GioHangCTDTO> lstgioHangCTDTO = new List<GioHangCTDTO>();
            // trường hợp người dùng chưa đăng nhập
            if (sessionUser == null)
            {
                ViewData["AnBtnThanhToan"] = "true";
                //lấy thông tin giỏ hàng chi tiết từ session
                var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                if (sessionGioHangCT != null)
                {
                    // convert về list giỏ hàng để thực hiện lấy thông tin sản phẩm khác hàng vừa thêm ở trang chủ
                    var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                    
                    if (thongTinGioHangCT != null)
                    {
                        foreach (var x in thongTinGioHangCT)
                        {
                            
                            // lấy dữ liệu của giỏ hàng session vừa convert về và truy
                            // vấn lấy ra dữ liệu tên sản phầm cho phầm hiển thị lên trên giỏ hàng
                            GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                            {
                                GioHangId = x.Id,
                                Id = x.Id,
                                Price = x.Price,
                                ProductId = x.ProductId,
                                ImgURL = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ImgURL,
                                ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                Quantity = x.Quantity,
                                Status = x.Status,
                            };
                            
                            //thêm vào giỏ hàng để hiển thị ra bên ngoài
                            lstgioHangCTDTO.Add(gioHangCTDTO);
                        }
                    }
                }
                var jsonDataGioHangView = JsonConvert.SerializeObject(lstgioHangCTDTO);
                HttpContext.Session.SetString("GioHangView", jsonDataGioHangView);
                GioHangDTO gioHang = new GioHangDTO();
                gioHang.GioHangChiTiet = lstgioHangCTDTO;

                
                foreach (var x in lstgioHangCTDTO)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }
                ViewData["TongTien"] = tongTien; 
               
                return View(gioHang);

            }
            else
            {
                ViewData["AnBtnThanhToan"] = "false";
                // trường hợp người dùng đã đăng nhập
                var Id = HttpContext.Session.GetString("UserId");
                //kiểm tra xem khách hàng này đã mua sản phầm nào trước đos hay chưa nếu có thì tiếp tục kiểm tra
                //lúc người ta chưa đăng nhập có mua sản phẩm nào nữa không
                if (_context.gioHangs.Any(c=>c.UserId== Guid.Parse(Id) && c.Status==0))
                {
                    //lấy ra id giỏ hàng của user
                    var idGioHang = _context.gioHangs.FirstOrDefault(c => c.UserId == Guid.Parse(Id) && c.Status==0).Id;
                    // kiểm tra xem session giỏ hàng có dữ liệu hay không 
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    if (sessionGioHangCT != null)
                    {
                        //convert giỏ hàng session về dạng list
                        var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                        if (thongTinGioHangCT != null)
                        {
                            // kiểm tra qua hết 1 lượt của giỏ hàng session xem có sản phẩm nào đã
                            // từng mua trước đó hay chưa nếu từng mua rồi thì cộng dồn số lượng lên
                            foreach (var x in thongTinGioHangCT)
                            {
                                //kiểm tra nếu sản phẩm trong giỏ hàng session đã từng mua thì sẽ dộng dồn số lượng
                                if (_context.gioHangCTs.Any(c => c.GioHangId == idGioHang && c.ProductId == x.ProductId))
                                {
                                    foreach (var item in _context.gioHangCTs.Where(c => c.GioHangId == idGioHang && c.ProductId == x.ProductId))
                                    {
                                        // cộng dồn số luợng được luư trong giỏ hàng database và số lượng của sản phẩm đó trong giỏ hàng session
                                        item.Quantity = item.Quantity + x.Quantity;

                                    }
                                }
                                else
                                {
                                    // nếu sản phẩm trong giỏ hàng session chưa
                                    // từng được mua thì tiến hành thêm mới sản phẩm vào cả giỏ hàng db
                                    GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                    {
                                        GioHangId = x.GioHangId,
                                        Id = Guid.NewGuid(),
                                        Price = x.Price,
                                        ProductId = x.ProductId,
                                        ImgURL = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ImgURL,
                                        ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                        Quantity = x.Quantity,
                                        Status = x.Status
                                    };

                                    _context.gioHangCTs.Add(x);
                                    _context.SaveChanges();
                                    // thêm sản phầm vào list giỏ hàng có thêm tên của sản phẩm để hiển thị ra ngoài view
                                    lstgioHangCTDTO.Add(gioHangCTDTO);


                                }

                            }
                            //kiểm tra xem dữ liệu trong giỏ hàng của user nếu có thì add thêm vào list giỏ hàng bên trên để hiển thị ra ngoài view
                            var lstGiohangCT = _context.gioHangCTs.Where(c => c.GioHangId == idGioHang && c.Status==0).ToList();
                            if (lstGiohangCT != null)
                            {
                                foreach (var x in lstGiohangCT)
                                {
                                    GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                    {
                                        GioHangId = x.Id,
                                        Id = x.Id,
                                        Price = x.Price,
                                        ProductId = x.ProductId,
                                        ImgURL = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ImgURL,
                                        ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                        Quantity = x.Quantity,
                                        Status = x.Status,
                                    };

                                    lstgioHangCTDTO.Add(gioHangCTDTO);
                                }
                            }

                        }
                       
                    }
                    else
                    {
                        
                        var lstGiohangCT = _context.gioHangCTs.Where(c => c.GioHangId == idGioHang && c.Status==0).ToList();
                        if (lstGiohangCT != null)
                        {
                            foreach (var x in lstGiohangCT)
                            {
                                GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                {
                                    GioHangId = x.Id,
                                    Id = x.Id,
                                    Price = x.Price,
                                    ProductId = x.ProductId,
                                    ImgURL = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ImgURL,
                                    ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                    Quantity = x.Quantity,
                                    Status = x.Status,
                                };

                                lstgioHangCTDTO.Add(gioHangCTDTO);
                            }
                        }

                    }
                    //khi đã thêm xong dữ liệu của giỏ hàng session thì xoá hết dữ liệu này đi
                    HttpContext.Session.Remove("userGioHangCT");
                    HttpContext.Session.Remove("userGioHang");
                    //
                    var jsonDataGioHangView = JsonConvert.SerializeObject(lstgioHangCTDTO);
                    HttpContext.Session.SetString("GioHangView", jsonDataGioHangView);
                    GioHangDTO gioHang = new GioHangDTO();
                    gioHang.GioHangChiTiet = lstgioHangCTDTO;
                    
                   
                    foreach (var x in lstgioHangCTDTO)
                    {
                        tongTien = tongTien + (x.Quantity * x.Price);
                    }
                    ViewData["TongTien"] = tongTien;
                    HttpContext.Session.SetString("User",sessionUser); 
                    HttpContext.Session.SetString("UserId",sessionUserId);
                    return View(gioHang);
                }
                else
                {
                    //trường hợp trước đó người dùng chưa từng mua sản phẩm nào thì check xem giỏ hàng session của user
                    //đó có dữ liệu không nếu có thì add thêm vào list đẻ hiển thị ra ngoài vjew
                    var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    //check xem nếu người dùng đã có giỏ hàng session trước đó thì gán lại dữ liệu cho người dùng để thêm vào db
                    if (sessionGioHang != null)
                    {
                        //lấy thông tin  của giỏ hàng session để thêm vào database
                        var thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);
                        // gán lại Id của user cho giỏ hàng
                        if (_context.gioHangs.Any(c => c.UserId == Guid.Parse(sessionUserId) && c.Status==0) == false)
                        {
                            thongTinGioHang.Id = Guid.NewGuid();
                            thongTinGioHang.UserId = Guid.Parse(sessionUserId);
                            thongTinGioHang.TotalMoney = 0;
                            thongTinGioHang.Status = 0;
                            thongTinGioHang.FullName = "";
                            thongTinGioHang.Address = "";
                            thongTinGioHang.Email = "";
                            thongTinGioHang.PhoneNumber = "";
                        
                            _context.gioHangs.Add(thongTinGioHang);
                            _context.SaveChanges();
                        }
                    }
                    //kiểm tra xem giỏ hàng chi tiết session có dữ liệu hay không 
                    if (sessionGioHangCT != null)
                    {
                        var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                        //nếu có dữ liệu thì tiến hành thêm dữ liệu của giỏ hàng session vào list và vào database để hiển thị lên phần view
                        if (thongTinGioHangCT != null)
                        {
                            foreach (var x in thongTinGioHangCT)
                            {
                                
                                // GioHangCTDTO có các thông tin của bảng GioHangCT và có thêm trường ProductName
                                GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                {
                                    GioHangId = x.Id,
                                    Id = x.Id,
                                    Price = x.Price,
                                    ProductId = x.ProductId,
                                    ImgURL = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ImgURL,
                                    ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                    Quantity = x.Quantity,
                                    Status = x.Status,
                                };
                                
                                _context.gioHangCTs.Add(x);
                                _context.SaveChanges();
                                  //thêm dữ liệu vào list giỏ hàng
                                lstgioHangCTDTO.Add(gioHangCTDTO);
                            }
                         
                        }
                    }
                }
               //khi thêm xong thì xoá hết dữ liệu ở trong session này đi
                HttpContext.Session.Remove("userGioHangCT");
                HttpContext.Session.Remove("userGioHang");
                //
                
                var jsonData = JsonConvert.SerializeObject(lstgioHangCTDTO);
                HttpContext.Session.SetString("GioHangView", jsonData);
                
                GioHangDTO gioHangdto = new GioHangDTO();
                gioHangdto.GioHangChiTiet = lstgioHangCTDTO;

                
                foreach (var x in lstgioHangCTDTO)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }
                ViewData["TongTien"] = tongTien;
                HttpContext.Session.SetString("User",sessionUser); 
                HttpContext.Session.SetString("UserId",sessionUserId);
                return View(gioHangdto);
            }
        }
        [HttpPost]
        public IActionResult TangSoLuong(string idSanPham,int soluong,GioHangDTO giohang)
        {
            var sessionGioHang = HttpContext.Session.GetString("GioHangView");
            var thongTinGioHangView = JsonConvert.DeserializeObject<List<GioHangCTDTO>>(sessionGioHang);
            //check số lượng còn lại 
            decimal tongTien = 0;
            GioHangDTO gioHangdto = new GioHangDTO();
            var spCheck = _context.SanPhams.FirstOrDefault(c => c.id == Guid.Parse(idSanPham));
            if (spCheck.quantity < soluong + 1)
            {
                ViewData["CheckSoLuong"] =$@"Sản phẩm {spCheck.ProductName} chỉ còn {spCheck.quantity} mặt hàng";
                foreach (var x in thongTinGioHangView)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }
                ViewData["TongTien"] = tongTien;
                gioHangdto = giohang;
                gioHangdto.GioHangChiTiet = thongTinGioHangView;
                return View("Index", gioHangdto);
            }
            
            
            if (_context.SanPhams.FirstOrDefault(c => c.id == Guid.Parse(idSanPham)).quantity>0)
            {
              
                foreach(var x in thongTinGioHangView.Where(c => c.ProductId == Guid.Parse(idSanPham))){
                    x.Quantity = x.Quantity + 1;
                }
                //
                
                foreach (var x in thongTinGioHangView)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }
                ViewData["TongTien"] = tongTien;
                var jsonData = JsonConvert.SerializeObject(thongTinGioHangView);
                HttpContext.Session.SetString("GioHangView", jsonData);
                
            }
             
           
            gioHangdto = giohang;
            gioHangdto.GioHangChiTiet = thongTinGioHangView;
            
            return View("Index", gioHangdto);
        } 
        [HttpPost]
        public IActionResult GiamSoLuong(string idSanPham,int soluong,GioHangDTO giohang)
        {
            var sessionGioHang = HttpContext.Session.GetString("GioHangView");
            var thongTinGioHangView = JsonConvert.DeserializeObject<List<GioHangCTDTO>>(sessionGioHang);
            decimal tongTien = 0;
            GioHangDTO gioHangdto = new GioHangDTO();
            var spCheck = _context.SanPhams.FirstOrDefault(c => c.id == Guid.Parse(idSanPham));
            if (soluong-1<0)
            {
                foreach (var x in thongTinGioHangView)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }
                gioHangdto = giohang;
                gioHangdto.GioHangChiTiet = thongTinGioHangView;
                return View("Index", gioHangdto);
            }
            if (_context.SanPhams.FirstOrDefault(c => c.id == Guid.Parse(idSanPham)).quantity > 0)
            {
               
                foreach (var x in thongTinGioHangView.Where(c => c.ProductId == Guid.Parse(idSanPham))){
                    x.Quantity = x.Quantity - 1;
                    
                }
                //
                
                foreach (var x in thongTinGioHangView)
                {
                    tongTien = tongTien + (x.Quantity * x.Price);
                }

                ViewData["TongTien"] = tongTien;
                var jsonData = JsonConvert.SerializeObject(thongTinGioHangView);
                HttpContext.Session.SetString("GioHangView", jsonData);
            }
            gioHangdto = giohang;
            gioHangdto.GioHangChiTiet = thongTinGioHangView;
            
         
            return View("Index", gioHangdto);
          
        }
        [HttpPost]
        public IActionResult ThanhToan(GioHangDTO GioHangThanhToan)
        {
          
            var sessionGioHang = HttpContext.Session.GetString("GioHangView");
            var thongTinGioHangView = JsonConvert.DeserializeObject<List<GioHangCTDTO>>(sessionGioHang);
            var sessionUserId = HttpContext.Session.GetString("UserId");
            if (sessionUserId != null)
            {
                var GioHangUser = _context.gioHangs.FirstOrDefault(c => c.UserId == Guid.Parse(sessionUserId) && c.Status==0);
                var GioHangCTUser = _context.gioHangCTs.Where(c => c.GioHangId == GioHangUser.Id).ToList();

                GioHangUser.FullName = GioHangThanhToan.FullName;
                GioHangUser.Address = GioHangThanhToan.Address;
                GioHangUser.Status = 1;
                GioHangUser.Email = GioHangThanhToan.Email;
                GioHangUser.PhoneNumber = GioHangThanhToan.PhoneNumber;
           
                foreach (var x in thongTinGioHangView)
                {
                    GioHangUser.TotalMoney = GioHangUser.TotalMoney + (x.Quantity * x.Price);
                }
            
                //
          
                foreach (var x in thongTinGioHangView)
                {
                    foreach (var ct in GioHangCTUser.Where(ct=>ct.ProductId==x.ProductId))
                    {
                        ct.Quantity = x.Quantity;
                        ct.Status = 1;//đã thanh toán
                        var spGiamSoLuong = _context.SanPhams.FirstOrDefault(c => c.id == ct.ProductId);
                        spGiamSoLuong.quantity = spGiamSoLuong.quantity - x.Quantity;
                        _context.SanPhams.Update(spGiamSoLuong);
                    }
                }
                _context.gioHangs.Update(GioHangUser);
                _context.SaveChanges();
                ViewData["TongTien"] = 0;
                GioHangDTO gioHangDto = new GioHangDTO();
                gioHangDto.GioHangChiTiet = new List<GioHangCTDTO>();
                return View("Index",gioHangDto);
            }
            GioHangDTO gioHangDtos = new GioHangDTO();
            gioHangDtos.GioHangChiTiet = new List<GioHangCTDTO>();
            return View("Index",gioHangDtos);
        }
    }
}
