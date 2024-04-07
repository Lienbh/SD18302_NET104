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

        /// <summary>
        /// lien thay doi
        /// </summary>

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
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");
            List<GioHangCTDTO> lstgioHangCTDTO = new List<GioHangCTDTO>();
            if (sessionUser == null)
            {
                //lấy thông tin giỏ hàng chi tiết từ session
                var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                if (sessionGioHangCT != null)
                {
                    var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);

                    if (thongTinGioHangCT != null)
                    {
                        foreach (var x in thongTinGioHangCT)
                        {
                            GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                            {
                                GioHangId = x.Id,
                                Id = x.Id,
                                Price = x.Price,
                                ProductId = x.ProductId,
                                ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                Quantity = x.Quantity,
                                Status = x.Status,
                            };
                            lstgioHangCTDTO.Add(gioHangCTDTO);
                        }
                    }
                }
                



                return View(lstgioHangCTDTO);

            }
            else
            {
                var Id = HttpContext.Session.GetString("UserId");
                if (_context.gioHangs.Any(c=>c.UserId== Guid.Parse(Id)))
                {
                    var idGioHang = _context.gioHangs.FirstOrDefault(c => c.UserId == Guid.Parse(Id)).Id;
                    // kiểm tra xem session có dữ liệu hay không
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    if (sessionGioHangCT != null)
                    {
                        var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);
                        if (thongTinGioHangCT != null)
                        {
                            foreach (var x in thongTinGioHangCT)
                            {
                                if (_context.gioHangCTs.Any(c => c.GioHangId == idGioHang && c.ProductId == x.ProductId))
                                {
                                    foreach (var item in _context.gioHangCTs.Where(c => c.GioHangId == idGioHang && c.ProductId == x.ProductId))
                                    {
                                        item.Quantity = item.Quantity + x.Quantity;

                                    }
                                }
                                else
                                {

                                    GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                    {
                                        GioHangId = x.GioHangId,
                                        Id = Guid.NewGuid(),
                                        Price = x.Price,
                                        ProductId = x.ProductId,
                                        ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                        Quantity = x.Quantity,
                                        Status = x.Status
                                    };

                                    _context.gioHangCTs.Add(x);
                                    _context.SaveChanges();
                                    lstgioHangCTDTO.Add(gioHangCTDTO);


                                }

                            }
                            var lstGiohangCT = _context.gioHangCTs.Where(c => c.GioHangId == idGioHang).ToList();
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
                        var lstGiohangCT = _context.gioHangCTs.Where(c => c.GioHangId == idGioHang).ToList();
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
                                    ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                    Quantity = x.Quantity,
                                    Status = x.Status,
                                };

                                lstgioHangCTDTO.Add(gioHangCTDTO);
                            }
                        }

                    }
                    HttpContext.Session.Remove("userGioHangCT");
                    HttpContext.Session.Remove("userGioHang");

                    return View(lstgioHangCTDTO);
                }
                else
                {
                    var sessionGioHang = HttpContext.Session.GetString("userGioHang");
                    var sessionGioHangCT = HttpContext.Session.GetString("userGioHangCT");
                    if (sessionGioHang != null)
                    {
                        var thongTinGioHang = JsonConvert.DeserializeObject<GioHang>(sessionGioHang);
                        thongTinGioHang.UserId = Guid.Parse(Id);
                        _context.gioHangs.Add(thongTinGioHang);
                        _context.SaveChanges();
                    }
                    if (sessionGioHangCT != null)
                    {
                        var thongTinGioHangCT = JsonConvert.DeserializeObject<List<GioHangCT>>(sessionGioHangCT);

                        if (thongTinGioHangCT != null)
                        {
                            foreach (var x in thongTinGioHangCT)
                            {
                                GioHangCTDTO gioHangCTDTO = new GioHangCTDTO()
                                {
                                    GioHangId = x.Id,
                                    Id = x.Id,
                                    Price = x.Price,
                                    ProductId = x.ProductId,
                                    ProductName = _context.SanPhams.FirstOrDefault(c => c.id == x.ProductId).ProductName,
                                    Quantity = x.Quantity,
                                    Status = x.Status,
                                };
                                
                                _context.gioHangCTs.Add(x);
                                _context.SaveChanges();
                             
                                lstgioHangCTDTO.Add(gioHangCTDTO);
                            }
                            HttpContext.Session.Remove("userGioHangCT");
                            HttpContext.Session.Remove("userGioHang");
                        }
                    }
                }
               

                return View(lstgioHangCTDTO);
            }
        }

        // GET: GioHangController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GioHangController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GioHangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GioHangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GioHangController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GioHangController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
