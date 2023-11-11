using System;
using System.Collections.Generic;

namespace websitebanhang.Models
{
    public class OrderModel
    {
        public int Id { get; set; } // ID của đơn hàng
        public string Name { get; set; } // Tên đơn hàng
        public int UserId { get; set; } // ID của người dùng đặt hàng
        public decimal TotalPrice { get; set; } // Tổng giá trị đơn hàng
        public DateTime CreatedOnUtc { get; set; } // Thời gian tạo đơn hàng
        public List<OrderDetailModel> OrderDetails { get; set; } // Danh sách chi tiết đơn hàng
    }
}
