using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using websitebanhang.Context;
namespace websitebanhang.Models
{
    public class HomeModel
    {
        public List<tb_Product> ListProduct { get; set; }
        public List<tb_Category> ListCategory { get; set; }
        public List<tb_Slider> ListSlider { set; get; }
    }
}