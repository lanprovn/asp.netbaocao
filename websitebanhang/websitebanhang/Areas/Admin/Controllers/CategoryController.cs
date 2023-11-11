using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using websitebanhang.Context;

namespace websitebanhang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();

        // GET: Admin/Category
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Lấy danh sách danh mục từ cơ sở dữ liệu và truyền nó đến view theo phân trang
            var categoryList = objwebsiteBHEntities.tb_Category.ToList().ToPagedList(pageNumber, pageSize);
            return View(categoryList);
        }

        // Action để hiển thị chi tiết danh mục
        public ActionResult Details(int id)
        {
            // Lấy thông tin danh mục từ cơ sở dữ liệu dựa trên id
            var category = objwebsiteBHEntities.tb_Category.FirstOrDefault(c => c.Id == id);

            // Kiểm tra xem danh mục có tồn tại hay không
            if (category == null)
            {
                // Nếu không tìm thấy danh mục, điều hướng đến trang 404 hoặc trang thông báo lỗi
                return HttpNotFound(); // Trang 404 Not Found
            }

            // Nếu danh mục tồn tại, truyền thông tin danh mục đến view
            return View(category);
        }

        // Action để hiển thị form thêm danh mục
        public ActionResult Create()
        {
            return View();
        }

        // Action để xử lý việc thêm danh mục
        [HttpPost]
        public ActionResult Create(tb_Category category)
        {
            if (ModelState.IsValid)
            {
                // Thêm danh mục vào cơ sở dữ liệu và lưu
                objwebsiteBHEntities.tb_Category.Add(category);
                objwebsiteBHEntities.SaveChanges();

                // Điều hướng đến trang danh sách danh mục sau khi thêm thành công
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // Action để hiển thị form sửa danh mục
        public ActionResult Edit(int id)
        {
            var category = objwebsiteBHEntities.tb_Category.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        // Action để xử lý việc sửa danh mục
        [HttpPost]
        public ActionResult Edit(tb_Category category)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin danh mục trong cơ sở dữ liệu và lưu
                var existingCategory = objwebsiteBHEntities.tb_Category.FirstOrDefault(c => c.Id == category.Id);
                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                    // Cập nhật các thông tin danh mục khác tương tự
                    objwebsiteBHEntities.SaveChanges();
                }

                // Điều hướng đến trang danh sách danh mục sau khi sửa thành công
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // Action để hiển thị form xoá danh mục
        public ActionResult Delete(int id)
        {
            var category = objwebsiteBHEntities.tb_Category.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        // Action để xử lý việc xoá danh mục
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var category = objwebsiteBHEntities.tb_Category.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                // Xoá danh mục khỏi cơ sở dữ liệu và lưu
                objwebsiteBHEntities.tb_Category.Remove(category);
                objwebsiteBHEntities.SaveChanges();
            }

            // Điều hướng đến trang danh sách danh mục sau khi xoá thành công
            return RedirectToAction("Index");
        }
    }
}
