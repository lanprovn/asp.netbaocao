namespace websitebanhang.Context
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class tb_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Avatar { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ShortDes { get; set; }
        public string FullDescription { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PriceDiscount { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Slug { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
    }
}
