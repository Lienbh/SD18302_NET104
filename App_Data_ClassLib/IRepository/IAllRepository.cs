using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data_ClassLib.IRepository
{
    internal interface IAllRepository<T> where T : class
    {
        public ICollection<T> GetAll(); // Lấy ra tất cả
        public T GetByID(dynamic id); // Type - Lấy bởi ID
        public bool CreateObj(T obj); //Tạo mới và thêm vào Db
        public bool UpdateObj(T obj); //Sửa và lưu lại vào trong Db
        public bool DeleteObj(dynamic id); //Xóa đối tượng trong Db
    }
}
