namespace App_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(15); // Khai báo khoảng thời gian để 
                // Session timeout 
            }); // Khai bái dịch vụ cho Session
            // 1 phiên làm việc cho tới khi timeout sẽ được tính từ khi có request cuối cùng cho tới
            // khi hết thời gian timeout mà không có tác vụ khác chèn vào, nếu có, bộ đếm thời gian sẽ
            // reset. Dữ liệu được lưu vào web server
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); // Sử dụng Session
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}");// Set route mặc định

            app.Run();
        }
    }
}