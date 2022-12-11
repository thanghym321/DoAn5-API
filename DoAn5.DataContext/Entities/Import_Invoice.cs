using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Import_Invoice
    {
        public int Id { get; set; }
        public DateTime Import_Date { get; set; }
        public int User_Id { get; set; }
        public int Provider_Id { get; set; }
        public decimal Total { get; set; }
    }
}
