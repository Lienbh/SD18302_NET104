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
            var SpData = _reps.GetAll();
            return View(SpData);
        }
        [HttpPost]
        public IActionResult AddProductToCart(string idSanPham)
        {
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");
            var spUpdate = _context.SanPhams.ToList().Find(c=>c.id==Guid.Parse(idSanPham));
            if (sessionUser == null)
            {
                var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                if (sessionGioHang != null)
                {
                   
          
                    var thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);
                
         
                 
                    var sessionLstGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    var GioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionLstGioHangCT);
                    if (GioHangCT != null)
                    {
                        if(GioHangCT.Any(c=>c.ProductId== Guid.Parse(idSanPham)))
                        {
                            foreach(var x in GioHangCT.Where(c=>c.ProductId == Guid.Parse(idSanPham))){
                                x.Quantity = x.Quantity + 1;
                            }
                        }
                        else
                        {
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
                        
                        var jsonDataGioHangCT = JsonConvert.SerializeObject(GioHangCT);
                        HttpContext.Session.SetString("userGioHangCT", jsonDataGioHangCT);
                    }
                }
                else
                {
                    GioHang gioHang = new GioHang()
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.NewGuid()
                    };

                    var jsonData = JsonConvert.SerializeObject(gioHang);
                    HttpContext.Session.SetString("userGioHang", jsonData);

                    GioHangCT gioHangCT = new GioHangCT()
                    {
                        Id = Guid.NewGuid(),
                        GioHangId = gioHang.Id,
                        ProductId = Guid.Parse( idSanPham),
                        Quantity = 1,
                        Price = spUpdate.price,
                        Status = 0
                    };
                  
                  
                    List<GioHangCT> gioHangCTs = new List<GioHangCT>();
                    gioHangCTs.Add(gioHangCT);
                    var jsonDataGioHangCT = JsonConvert.SerializeObject(gioHangCTs);
                    HttpContext.Session.SetString("userGioHangCT", jsonDataGioHangCT);
                }

            }
            else
            {
                var Id = HttpContext.Session.GetString("UserId");
                var thongtingiohangByUserId = _context.gioHangs.ToList().Where(c => c.UserId == Guid.Parse(Id)).FirstOrDefault();
                if (thongtingiohangByUserId != null)
                {
                
                    GioHangCT gioHangCT = new GioHangCT()
                    {
                        Id = Guid.NewGuid(),
                        GioHangId = thongtingiohangByUserId.Id,
                        ProductId = Guid.Parse(idSanPham),
                        Quantity = 1,
                        Price = spUpdate.price,
                        Status = 0
                    };

                    if(_context.gioHangCTs.Any(c=>c.GioHangId==thongtingiohangByUserId.Id && c.ProductId == Guid.Parse(idSanPham)))
                    {
                        foreach (var x in _context.gioHangCTs.Where(c=> c.GioHangId == thongtingiohangByUserId.Id && c.ProductId == Guid.Parse(idSanPham))){
                            x.Quantity = x.Quantity + 1;
                        }
                    }
                    _ghct.Add(gioHangCT);

                }
                else
                {
                    //lấy thông tin giỏ hàng chi tiết
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                    //lấy thông tin giỏ hàng 
                    var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                    var thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);

                    if (thongTinGioHangCT != null)
                    {
                        thongTinGioHang.UserId = Guid.Parse(sessionUserId);

                        _gh.Add(thongTinGioHang);
                       
                    }
                
                    GioHangCT gioHangCT = new GioHangCT()
                    {
                        Id = Guid.NewGuid(),
                        GioHangId = thongtingiohangByUserId.Id,
                        ProductId = Guid.Parse(idSanPham),
                        Quantity = 1,
                        Price = spUpdate.price,
                        Status = 0
                    };
                    _ghct.Add(gioHangCT);

                }
               
            }
            // lấy data
            var SpData = _reps.GetAll();
            return View("Index", SpData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}