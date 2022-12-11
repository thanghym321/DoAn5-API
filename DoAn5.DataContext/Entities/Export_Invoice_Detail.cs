using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Export_Invoice_Detail
    {
        public int Id { get; set; }
        public int Export_Invoice_Id { get; set; }
        public int Product_Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
