using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Export_Invoice
    {
        public int Id { get; set; }
        public DateTime Export_Date { get; set; }
        public int Customer_Id { get; set; }
        public StatusEI Status { get; set; }
    }
}
