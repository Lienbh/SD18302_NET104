using App_Data_ClassLib.Models;
using App_Data_ClassLib.Repository;
using App_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace App_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        AllRepository<SanPham> _reps;
        AllRepository<GioHang> _repGioHang;
        AllRepository<GioHangCT> _repGioHangCT;
        SD18302_NET104Context _context;
        DbSet<SanPham> _sp;
        DbSet<GioHang> _gh;
        DbSet<GioHangCT> _ghct;



        public HomeController(ILogger<HomeController> logger)
        {
            _context = new SD18302_NET104Context();
            _sp = _context.SanPhams;
            _reps = new AllRepository<SanPham>(_sp, _context);
            _repGioHang = new AllRepository<GioHang>(_gh, _context);
            _repGioHangCT = new AllRepository<GioHangCT>(_ghct, _context);
            _logger = logger;
        }

        public IActionResult Index()
        {
            var SpData = _reps.GetAll().Where(c=>c.status!=0);
            return View(SpData);
        }
        // thêm sản phầm vào giỏ hàng
        [HttpPost]
        public IActionResult AddProductToCart(string idSanPham)
        {
            //kiểm tra xem user đó đã đăng nhập hay chưa
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");
            
            //lấy ra thông tin của sản phẩm muốn thêm vào giỏ hàng
            var spUpdate = _context.SanPhams.ToList().Find(c=>c.id==Guid.Parse(idSanPham));
            //TH user chưa đăng nhập
            if (sessionUser == null)
            {
                // lấy dữ liệu của giỏ hàng session
                var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                // nếu giỏ hàng đã có dữ liệu
                if (sessionGioHang != null)
                {
                    var thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);
                    //lấy dữ liệu của giỏ hàng chi tiết rồi convert về list
                    var sessionLstGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    var GioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionLstGioHangCT);
                    
                    if (GioHangCT != null)
                    {
                        //check xem nếu giỏ hàng đã có sản phẩm đó rồi thì số lượng sẽ cập nhật lên thêm 1 sản phẩm
                        if(GioHangCT.Any(c=>c.ProductId== Guid.Parse(idSanPham) && c.Status==0))
                        {
                            foreach(var x in GioHangCT.Where(c=>c.ProductId == Guid.Parse(idSanPham) && c.Status==0) ){
                                x.Quantity = x.Quantity + 1;
                               
                              
                            }
                        }
                        else
                        {
                            //nếu chưa có thì thêm mới sản phầm này vào giỏ hàng
                            GioHangCT gioHangCT = new GioHangCT()
                            {
                                Id = Guid.NewGuid(),
                                GioHangId = thongTinGioHang.Id,
                                ProductId = spUpdate.id,
                                Quantity = 1,
                                Price = spUpdate.price,
                                Status = 0
                            };
                            GioHangCT.Add(gioHangCT);
                        }
                       
                        // convert  list giỏ hàng session thành string để  lưu lại vào session
                        var jsonDataGioHangCT = JsonConvert.SerializeObject(GioHangCT);
                        HttpContext.Session.SetString("userGioHangCT", jsonDataGioHangCT);
                    }
                }
                else
                {
                    //TH nêus người dùng chưa có gior hàng thì tạo giỏ hàng cho user đó
                        GioHang gioHang = new GioHang();
                    
                        gioHang.Id = Guid.NewGuid();
                        gioHang.UserId = Guid.NewGuid();
                        gioHang.TotalMoney = 0;
                        gioHang.Status = 0;
                        gioHang.FullName = "";
                        gioHang.Address = "";
                        gioHang.Email = "";
                        gioHang.PhoneNumber = "";
                        
                      
                        //lưu thông tin giỏ hàng vào session
                        var jsonData = JsonConvert.SerializeObject(gioHang);
                        HttpContext.Session.SetString("userGioHang", jsonData);
                    
                    
                   
                    // lưu sản phẩm vào giỏ hàng
                    GioHangCT gioHangCT = new GioHangCT()
                    {
                        Id = Guid.NewGuid(),
                        GioHangId = gioHang.Id,
                        ProductId = Guid.Parse( idSanPham),
                        Quantity = 1,
                        Price = spUpdate.price,
                        Status = 0
                    };
                  
                  // tạo ra 1 list giỏ hàng để thêm sản phẩm vừa thêm vào vào trong session
                    List<GioHangCT> gioHangCTs = new List<GioHangCT>();
                    gioHangCTs.Add(gioHangCT);
                    // convert list giỏ hàng để lưu lại vào trong session
                    var jsonDataGioHangCT = JsonConvert.SerializeObject(gioHangCTs);
                    HttpContext.Session.SetString("userGioHangCT", jsonDataGioHangCT);
                }

            }
            else
            {
                //TH user đã đăng nhập
                //lấy ra user id đã được lưu lại sau khi đăng nhập
                var Id = HttpContext.Session.GetString("UserId");
                // lấy ra thông tin giỏ hàng được lưu trong database xem có dữ liệu hay không 
                var thongtingiohangByUserId = _context.gioHangs.ToList().Where(c => c.UserId == Guid.Parse(Id) && c.Status==0).FirstOrDefault();
                //nếu có dữ liệu
                if (thongtingiohangByUserId != null)
                {
                
    
                    //kiểm tra xem trong giỏ hàng được luư trong database đã có sản phẩm này chưa nnếu có rồi thì số lượng sẽ cộng thêm 1
                    //nếu không có thì thêm mới sản phầm đó với số lượng là 1 vào trong giỏ hàng
                    if(_context.gioHangCTs.Any(c=>c.GioHangId==thongtingiohangByUserId.Id && c.ProductId == Guid.Parse(idSanPham) && c.Status==0))
                    {
                        foreach (var x in _context.gioHangCTs.Where(c=> c.GioHangId == thongtingiohangByUserId.Id 
                                                                        && c.ProductId == Guid.Parse(idSanPham) && c.Status==0)){
                            x.Quantity = x.Quantity + 1;
                            _context.gioHangCTs.Update(x);
                           
                        }
                    }
                    else {
                        
                        GioHangCT gioHangCT = new GioHangCT()
                        {
                            Id = Guid.NewGuid(),
                            GioHangId = thongtingiohangByUserId.Id,
                            ProductId = Guid.Parse(idSanPham),
                            Quantity = 1,
                            Price = spUpdate.price,
                            Status = 0
                        };
                        
                        //thêm sản phẩm vào giỏ hàng
                        _context.gioHangCTs.Add(gioHangCT);
                       
                   }
                    _context.SaveChanges();
                  
                }
                else
                {
                    //TH giỏ hàng không có dữ liệu 
                    //lấy thông tin giỏ hàng chi tiết
                    var thongTinGioHangCT = new List<GioHangCT>();
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    if (sessionGioHangCT != null)
                    {
                        thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                    }

                    var thongTinGioHang = new GioHang();
                    //lấy thông tin giỏ hàng 
                    var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                    if (sessionGioHang != null)
                    {
                        thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);
                    }
                    
                    if (_context.gioHangs.Any(c => c.UserId == Guid.Parse(sessionUserId)) == false)
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
                   
                   
                 //thêm sản phẩm vừa thêm vào trong giỏ hàng chi tiết của database
                    GioHangCT gioHangCT = new GioHangCT()
                    {
                        Id = Guid.NewGuid(),
                        GioHangId = thongTinGioHang.Id,
                        ProductId = Guid.Parse(idSanPham),
                        Quantity = 1,
                        Price = spUpdate.price,
                        Status = 0
                    };
                    _context.gioHangCTs.Add(gioHangCT);
                    _context.SaveChanges();

                }
               
            }
            // lấy data của sản phẩm để truyền ra view
            var SpData = _reps.GetAll().Where(c=>c.status!=0);
            return View("Index", SpData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}