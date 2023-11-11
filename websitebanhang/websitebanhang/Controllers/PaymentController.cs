using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitebanhang.Context;
using websitebanhang.Models;

namespace websitebanhang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBHEntities objwebsiteBHEntities = new WebsiteBHEntities();
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                tb_Order objOrder = new tb_Order();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objwebsiteBHEntities.tb_Order.Add(objOrder);
                objwebsiteBHEntities.SaveChanges();
                int intOrderId = objOrder.Id;

                List<tb_OrderDetail> lstOrderDetail = new List<tb_OrderDetail>();

                foreach (var item in lstCart)
                {
                    tb_OrderDetail obj = new tb_OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objwebsiteBHEntities.tb_OrderDetail.AddRange(lstOrderDetail);
                objwebsiteBHEntities.SaveChanges();

                // Retrieve the user's email
                var userId = int.Parse(Session["idUser"].ToString());
                var user = objwebsiteBHEntities.tb_Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    // Handle the case where the user is not found
                    // Return an appropriate response or redirect
                }
                var email = user.Email;

                // Save the email in the session
                Session["email"] = email;

                // Clear the cart in session
                Session.Remove("cart");

                return RedirectToAction("PaymentSuccess");
            }

        }
        public ActionResult PaymentSuccess()
        {
            Session["count"] = 0;
            return View();
        }
    }
}