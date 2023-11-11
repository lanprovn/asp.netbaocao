using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;
using websitebanhang.Models;

namespace websitebanhang.Controllers
{
    public class ProductCategoryController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();
        // GET: Courses
        public ActionResult Index()
        {
            ProductCategoryModel objProductsModel = new ProductCategoryModel();
            objProductsModel.ListProduct = objwebsiteBHEntities.tb_Product.ToList();
            return View(objProductsModel);
        }
    }
}