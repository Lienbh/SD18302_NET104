using Microsoft.AspNetCore.Mvc;
using App_Data_ClassLib;
using App_Data_ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using App_Data_ClassLib.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace App_MVC.Controllers
{
    public class UserController : Controller
    {
        //Khởi tạo DBContext
        SD18302_NET104Context _context;
        //Khởi tạo Resspository với 2 tham số là dbSet và DbContext
        AllRepository<User> _repo;
        DbSet<User> _users;
        //Lấy ra tất cả các bản ghi
        public UserController()
        {
            //Khởi tạo DBContext
            _context = new SD18302_NET104Context();
            // Khởi tạo repository với 2 tham số là DbSet và DBContext
            _users = _context.Users;
            _repo = new AllRepository<User>(_users, _context);

        }
       
        // Lấy ra tất cả danh sách Users
        public IActionResult Index(string name) // tham số name để tìm kiếm
        {
            var sessionData = HttpContext.Session.GetString("user");
            if (sessionData == null)
            {
                ViewData["message"] = "Bạn chưa đăng nhập hoặc phiên đăng nhập đã hết hạn";
            }
            else
            {
                ViewData["message"] = $"Chào mừng {sessionData} đến với unfinished square integer";
            }
            var userData = _repo.GetAll();
            if (string.IsNullOrEmpty(name))
            {
                return View(userData);
            }
            else
            {
                var searchData = _repo.GetAll().Where(x => x.Name.Contains(name)).ToList(); // Tìm theo tên           
                ViewData["count"] = searchData.Count;
                ViewBag.Count = searchData.Count;
                if (searchData.Count == 0) // Nếu ko tìm thấy 
                {
                    return View(userData);
                }
                else return View(searchData); // có tìm thấy
            }
        }
        
        public IActionResult Create() //Action để mở form điền thông tin user
        {
            return View();
        }
        // Action để thực hiện thêm vào DB
        [HttpPost]
        public IActionResult Create(User user)
        {
            user.ID = Guid.NewGuid();
            _repo.CreateUser(user);
            var userData = _repo.GetAll();
            var jsonData = JsonConvert.SerializeObject(userData);
             HttpContext.Session.SetString("User", jsonData);
            return RedirectToAction("Index");
        }
        // Sửa
        public IActionResult Edit(Guid id) //Form Load ra đối tượng cần sửa
        {
            //Lấy ra đối tượng cần sửa
            var updateUser = _repo.GetByID(id);
            return View(updateUser);
        }
        public IActionResult EditUser(User user) //Action này thực hiện thay đổi, khi cần thì trỏ tới nó
        {
            _repo.UpdateObj(user);
            return RedirectToAction("Index");
        }
        //Xóa
        public IActionResult Delete(Guid id)
        {
            var deleteUser = _repo.GetByID(id);
            var jsonData = JsonConvert.SerializeObject(deleteUser); //Ép kiểu sang json
            HttpContext.Session.SetString("delete", jsonData); //Cho dữ liệu vào session
            _repo.DeleteObj(id);
            return RedirectToAction("Index");
        }

        public IActionResult RollBack()
        {
            if (HttpContext.Session.Keys.Contains("deleted"))
            {
                var jsonData = HttpContext.Session.GetString("deleted");
                //Tạo mới đối tượng có dữ liệu y hệt như dữ liệu cũ
                var deletedUser = JsonConvert.DeserializeObject<User>(jsonData);
                _repo.CreateObj(deletedUser); // Add lại vào trong Db
                return RedirectToAction("Index"); // về trang index
            }
            else return Content("It's too late to apologize");
        }

        //Thông tin Details
        public IActionResult Details(Guid id)
        {
            var getUser = _repo.GetByID(id);
            return View(getUser);
        }

        public IActionResult Login()//Action này return về View để mở View cho phép nhập thông tin đăng nhập
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password) // Action này thực hiện đăng nhập
        {
            var user = _repo.GetAll().FirstOrDefault(p => p.Username == username && p.Password == password);
            if (user != null)
            {
                //return Content("Đăng nhập thất bại");
                //Dùng TempData để lưu trữ dữ liệu đăng nhập tạm thời 
                TempData["Login"] = username;
                //Lưu trữ thông tin đăng nhập vào Session
                HttpContext.Session.SetString("User", username);
                HttpContext.Session.SetString("UserId", user.ID.ToString());
               
                return RedirectToAction("Index", "Home");

            }
            else
            {
                
                return Content("Đăng nhập thất bại");
            }

        }
    }
}
