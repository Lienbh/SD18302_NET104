using App_Data_ClassLib.IRepository;
using App_Data_ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.Repository
{
    public class AllRepository<G> : IAllRepository<G> where G : class
    {
        SD18302_NET104Context context;
        DbSet<G> dbset; //CRUD trên DBset vì nó đại diện cho bảng
                        //Khi cần gọi lại và dùng thật thì lại cần chính xác nó là DbSet nào
                        //Lúc đó ta sẽ gán dbset = DbSet cần dùng
        public AllRepository()
        {
            context = new SD18302_NET104Context();
        }
        public AllRepository(DbSet<G> dbset, SD18302_NET104Context context)
        {
            this.dbset = dbset; //Gán lại khi dùng
            this.context = context;
        }
        public bool CreateObj(G obj)
        {
            try
            {

                dbset.Add(obj);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        } 
        public bool CreateUser(User obj)
        {
            try
            {
             
                  context.Users.Add(obj);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool DeleteObj(dynamic id)
        {
            try
            {
                //Tìm trong bảng đối tượng cần xóa
                var deleteObj = dbset.Find(id); //Find truyền vào thuộc tính
                //Chỉ sử dụng với PK
                dbset.Remove(deleteObj); //Xóa
                context.SaveChanges(); //Lưu lại
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public ICollection<G> GetAll()
        {
            return dbset.ToList();
        }

        public G GetByID(dynamic id)
        {
            return dbset.Find(id);
        }

        public bool UpdateObj(G obj)
        {
            try
            {

                dbset.Update(obj); //Sửa
                context.SaveChanges(); //Lưu lại
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}