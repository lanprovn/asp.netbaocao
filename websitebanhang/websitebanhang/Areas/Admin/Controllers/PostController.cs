using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using websitebanhang.Context;

namespace websitebanhang.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();

        // GET: Admin/Post
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // Lấy danh sách bài viết từ cơ sở dữ liệu và truyền nó đến view theo phân trang
            var postList = objwebsiteBHEntities.tb_Post.ToList().ToPagedList(pageNumber, pageSize);
            return View(postList);
        }

        // Action để hiển thị chi tiết bài viết
        public ActionResult Details(int id)
        {
            // Lấy thông tin bài viết từ cơ sở dữ liệu dựa trên id
            var post = objwebsiteBHEntities.tb_Post.FirstOrDefault(p => p.Id == id);

            // Kiểm tra xem bài viết có tồn tại hay không
            if (post == null)
            {
                // Nếu không tìm thấy bài viết, điều hướng đến trang 404 hoặc trang thông báo lỗi
                return HttpNotFound(); // Trang 404 Not Found
            }

            // Nếu bài viết tồn tại, truyền thông tin bài viết đến view
            return View(post);
        }

        // Action để hiển thị form thêm bài viết
        public ActionResult Create()
        {
            return View();
        }

        // Action để xử lý việc thêm bài viết
        [HttpPost]
        public ActionResult Create(tb_Post post)
        {
            if (ModelState.IsValid)
            {
                // Thêm bài viết vào cơ sở dữ liệu và lưu
                objwebsiteBHEntities.tb_Post.Add(post);
                objwebsiteBHEntities.SaveChanges();

                // Điều hướng đến trang danh sách bài viết sau khi thêm thành công
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // Action để hiển thị form sửa bài viết
        public ActionResult Edit(int id)
        {
            var post = objwebsiteBHEntities.tb_Post.FirstOrDefault(p => p.Id == id);
            return View(post);
        }

        // Action để xử lý việc sửa bài viết
        [HttpPost]
        public ActionResult Edit(tb_Post post)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin bài viết trong cơ sở dữ liệu và lưu
                var existingPost = objwebsiteBHEntities.tb_Post.FirstOrDefault(p => p.Id == post.Id);
                if (existingPost != null)
                {
                    existingPost.Title = post.Title;
                    existingPost.Content = post.Content;
                    existingPost.UpdatedOnUtc = DateTime.UtcNow;
                    // Cập nhật các thông tin bài viết khác tương tự
                    objwebsiteBHEntities.SaveChanges();
                }

                // Điều hướng đến trang danh sách bài viết sau khi sửa thành công
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // Action để hiển thị form xoá bài viết
        public ActionResult Delete(int id)
        {
            var post = objwebsiteBHEntities.tb_Post.FirstOrDefault(p => p.Id == id);
            return View(post);
        }

        // Action để xử lý việc xoá bài viết
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var post = objwebsiteBHEntities.tb_Post.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                // Xoá bài viết khỏi cơ sở dữ liệu và lưu
                objwebsiteBHEntities.tb_Post.Remove(post);
                objwebsiteBHEntities.SaveChanges();
            }

            // Điều hướng đến trang danh sách bài viết sau khi xoá thành công
            return RedirectToAction("Index");
        }
    }
}