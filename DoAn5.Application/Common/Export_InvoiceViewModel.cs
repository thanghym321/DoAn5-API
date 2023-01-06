using DoAn5.DataContext.Entities;
using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.Application.Common
{
    public class Export_InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime Export_Date { get; set; }
        public int Customer_Id { get; set; }
        public StatusEI Status { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
