using App_Data_ClassLib.Models;
using App_Data_ClassLib.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace App_MVC.Controllers
{
    public class SanPhamController : Controller
    {
        AllRepository<SanPham> _reps;
        SD18302_NET104Context _context;
        DbSet<SanPham> _sp;

        public SanPhamController()
        {
            _context = new SD18302_NET104Context();
            _sp = _context.SanPhams;
            _reps = new AllRepository<SanPham>(_sp, _context);
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
        public IActionResult Create(SanPham sp)
        {
            //Xây dựng 1 đường dẫn để lưu ảnh trong thư mục wwwroot
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", sp.ImgURL);
            //Kết quả thu được có dạng như sau: wwwroot/img/concho.pngs
            //Thực hiện việc sao chép fike được chọn vào thư mục root
            var stream = new FileStream(path, FileMode.Create);
            //Thực hiện sao chép ảnh vào thư mục root
        
          
            if (_context.SanPhams.Any(p => p.ProductName == sp.ProductName))
            {
                ModelState.AddModelError("ProductName", "Dữ liệu đã tồn tại");
                return View("Create", sp);
            }
            else
            {
                sp.id = Guid.NewGuid();
                _reps.CreateObj(sp);

            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var sp = _reps.GetByID(id);
            return View(sp);
        }
        [HttpPost]
        public IActionResult EditSp(SanPham sp)
        {
            try
            {
                //Xây dựng 1 đường dẫn để lưu ảnh trong thư mục wwwroot
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", sp.ImgURL);
                //Kết quả thu được có dạng như sau: wwwroot/img/concho.pngs
                //Thực hiện việc sao chép fike được chọn vào thư mục root
                var stream = new FileStream(path, FileMode.Create);
                //Thực hiện sao chép ảnh vào thư mục root


                if (_context.SanPhams.Any(p => p.ProductName == sp.ProductName && p.id != sp.id))
                {
                    ModelState.AddModelError("ProductName", "Dữ liệu đã tồn tại");
                    return View("Edit", sp);
                }
                else
                {
                 
                    _reps.UpdateObj(sp);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //Xóa
        public IActionResult Delete(int id)
        {
            return View();
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
