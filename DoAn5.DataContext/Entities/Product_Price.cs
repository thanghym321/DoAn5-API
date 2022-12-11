using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Product_Price
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public decimal Price { get; set; }
    }
}
