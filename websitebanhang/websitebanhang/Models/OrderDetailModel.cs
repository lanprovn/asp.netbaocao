public class OrderDetailModel
{
    public int Id { get; set; } // ID của chi tiết đơn hàng
    public int OrderId { get; set; } // ID của đơn hàng liên quan
    public int ProductId { get; set; } // ID của sản phẩm
    public int Quantity { get; set; } // Số lượng sản phẩm trong chi tiết đơn hàng
}
