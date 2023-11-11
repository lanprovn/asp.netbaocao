using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using websitebanhang.Context;

namespace websitebanhang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();

        // Action để hiển thị danh sách sản phẩm
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<tb_Product>();

            // Kiểm tra xem có từ khóa tìm kiếm không
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            // Lọc sản phẩm dựa trên từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objwebsiteBHEntities.tb_Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objwebsiteBHEntities.tb_Product.ToList();
            }

            ViewBag.CurrentFilter = SearchString;

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();

            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        // Action để hiển thị chi tiết sản phẩm
        public ActionResult Details(int id)
        {
            var product = objwebsiteBHEntities.tb_Product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // Action để hiển thị form thêm sản phẩm
        public ActionResult Create()
        {
            return View();
        }

        // Action để xử lý việc thêm sản phẩm
        [HttpPost]
        public ActionResult Create(tb_Product product)
        {
            if (ModelState.IsValid)
            {
                product.ShowOnHomePage = true; // Hiển thị sản phẩm trên trang chủ
                objwebsiteBHEntities.tb_Product.Add(product);
                objwebsiteBHEntities.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Action để hiển thị form sửa sản phẩm
        public ActionResult Edit(int id)
        {
            var product = objwebsiteBHEntities.tb_Product.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        // Action để xử lý việc sửa sản phẩm
        [HttpPost]
        public ActionResult Edit(tb_Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = objwebsiteBHEntities.tb_Product.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    // Cập nhật các thông tin sản phẩm khác tương tự
                    objwebsiteBHEntities.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Action để hiển thị form xoá sản phẩm
        public ActionResult Delete(int id)
        {
            var product = objwebsiteBHEntities.tb_Product.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        // Action để xử lý việc xoá sản phẩm
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var product = objwebsiteBHEntities.tb_Product.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                objwebsiteBHEntities.tb_Product.Remove(product);
                objwebsiteBHEntities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
