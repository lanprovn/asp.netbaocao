using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using websitebanhang.Context;

namespace websitebanhang.Models
{
    public class CartModel
    {
        public tb_Product Product { get; set; }
        public int Quantity { get; set; }
        public string Avatar { get; set; }
    }
}
