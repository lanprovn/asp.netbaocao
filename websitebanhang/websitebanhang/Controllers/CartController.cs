using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Models;
using websitebanhang.Context;

namespace websitebanhang.Controllers
{
    public class CartController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();

        public ActionResult Index()
        {
            var cart = (List<CartModel>)Session["cart"];
            return View(cart);
        }

        [HttpPost]
        public ActionResult SaveTotalPrice(decimal totalPrice)
        {
            Session["TotalPrice"] = totalPrice;
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult GetTotalPrice()
        {
            decimal totalPrice = (decimal)Session["TotalPrice"];
            return Json(new { totalPrice = totalPrice }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            var cart = (List<CartModel>)Session["cart"];

            if (cart == null)
            {
                cart = new List<CartModel>();
                var product = objwebsiteBHEntities.tb_Product.Find(id);
                cart.Add(new CartModel
                {
                    Product = product,
                    Quantity = quantity,
                    Avatar = product.Avatar // Lấy Avatar của sản phẩm
                });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    var product = objwebsiteBHEntities.tb_Product.Find(id);
                    cart.Add(new CartModel
                    {
                        Product = product,
                        Quantity = quantity,
                        Avatar = product.Avatar // Lấy Avatar của sản phẩm
                    });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

                Session["cart"] = cart;
            }

            return Json(new { Message = "Thành công" }, JsonRequestBehavior.AllowGet);
        }

        private int isExist(int id)
        {
            var cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult Remove(int Id)
        {
            var cart = (List<CartModel>)Session["cart"];
            cart.RemoveAll(x => x.Product.Id == Id);
            Session["cart"] = cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;

            return Json(new { Message = "Thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}
