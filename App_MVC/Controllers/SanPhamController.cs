﻿using App_Data_ClassLib.Models;
using App_Data_ClassLib.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace App_MVC.Controllers
{
    public class SanPhamController : Controller
    {
        AllRepository<SanPham> _reps;
        AllRepository<GioHangCT> _repsGioHangCT;
        SD18302_NET104Context _context;
        DbSet<SanPham> _sp;
        DbSet<GioHangCT> _spGioHangCT;

        public SanPhamController()
        {
            _context = new SD18302_NET104Context();
            _sp = _context.SanPhams;
            _reps = new AllRepository<SanPham>(_sp, _context);
            _repsGioHangCT = new AllRepository<GioHangCT>(_spGioHangCT, _context);
        }
        public IActionResult Index()
        {
            var SpData = _reps.GetAll();
            return View(SpData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp, IFormFile photo)
        {
           
            if (_context.SanPhams.Any(p => p.ProductName == sp.ProductName))
            {
                ModelState.AddModelError("ProductName", "Dữ liệu đã tồn tại");
                var sessionUser1 = HttpContext.Session.GetString("User");
                var sessionUserId1 = HttpContext.Session.GetString("UserId");

                HttpContext.Session.SetString("UserId", sessionUserId1);
                HttpContext.Session.SetString("User", sessionUser1);
                return View("Create", sp);
            }
            else
            {
                //Xây dựng 1 đường dẫn để lưu ảnh trong thư mục wwwroot
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", photo.FileName);
                //Kết quả thu được có dạng như sau: wwwroot/img/concho.pngs
                //Thực hiện việc sao chép fike được chọn vào thư mục root
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                //Thực hiện sao chép ảnh vào thư mục root
                sp.id = Guid.NewGuid();
                sp.ImgURL = photo.FileName;
                _reps.CreateObj(sp);

            }
            var allSP = _reps.GetAll(); ;
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");

            HttpContext.Session.SetString("UserId", sessionUserId);
            HttpContext.Session.SetString("User", sessionUser);
            return RedirectToAction("Index", allSP);
        
        }
        public IActionResult Edit(Guid id)
        {
            var sp = _reps.GetByID(id);
            return View(sp);
        }
        [HttpPost]
        public IActionResult EditSp(SanPham sp, IFormFile photo)
        {
            try
            {
                

                if (_context.SanPhams.Any(p => p.ProductName == sp.ProductName && p.id != sp.id))
                {
                    ModelState.AddModelError("ProductName", "Dữ liệu đã tồn tại");
                    return View("Edit", sp);
                }
                else
                {
                    if (photo != null)
                    {
                        //Xây dựng 1 đường dẫn để lưu ảnh trong thư mục wwwroot
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", photo.FileName);
                        //Kết quả thu được có dạng như sau: wwwroot/img/concho.pngs
                        //Thực hiện việc sao chép fike được chọn vào thư mục root

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            photo.CopyTo(stream);
                        }
                        sp.ImgURL = photo.FileName;
                    }
                    else
                    {
                        var spUpdate = _reps.GetByID(sp.id);
                        sp.ImgURL = spUpdate.ImgURL;
                    }
                    _reps.UpdateObj(sp);

                }
                var sessionUser = HttpContext.Session.GetString("User");
                var sessionUserId = HttpContext.Session.GetString("UserId");

                HttpContext.Session.SetString("UserId", sessionUserId);
                HttpContext.Session.SetString("User", sessionUser);
                var allSP= _reps.GetAll(); ;
                return RedirectToAction("Index", allSP);
            }
            catch
            {
                var sessionUser = HttpContext.Session.GetString("User");
                var sessionUserId = HttpContext.Session.GetString("UserId");

                HttpContext.Session.SetString("UserId", sessionUserId);
                HttpContext.Session.SetString("User", sessionUser);
                var allSP = _reps.GetAll(); ;
                return RedirectToAction("Index", allSP);
            }
        }
       
        public IActionResult Delete(Guid id)
        {
            var spDelete = _reps.GetByID(id);
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");

            HttpContext.Session.SetString("UserId", sessionUserId);
            HttpContext.Session.SetString("User", sessionUser);
            return View(spDelete);
        }
        //Xóa
        public IActionResult ConfirmDelete(Guid id)
        {
              var lstGioHang=  _context.gioHangCTs.Where(c=>c.ProductId== id).ToList();
            _context.gioHangCTs.RemoveRange(lstGioHang);
            var spDelete = _reps.DeleteObj(id);
            var allSP = _reps.GetAll(); ;
            var sessionUser = HttpContext.Session.GetString("User");
            var sessionUserId = HttpContext.Session.GetString("UserId");

            HttpContext.Session.SetString("UserId", sessionUserId);
            HttpContext.Session.SetString("User", sessionUser);
            return RedirectToAction("Index", allSP);
         
        }
        [HttpPost]
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
        //Thông tin Details
        public IActionResult Details(int id)
        {

            return View();
        }

        //public IActionResult AddGH(GioHang gioHang) 
        //{
        //    GioHang.UpdateObj(gioHang);
        //    return RedirectToAction("Index");
        //}
    }
}
