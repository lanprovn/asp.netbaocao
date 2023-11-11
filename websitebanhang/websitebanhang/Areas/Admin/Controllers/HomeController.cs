using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;

namespace websitebanhang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();
        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm cho trang chủ
            List<tb_Product> homeProducts = objwebsiteBHEntities.tb_Product.Where(p => (bool)p.ShowOnHomePage).ToList();

            // Truyền danh sách sản phẩm đến view
            return View(homeProducts);
        }
    }

}