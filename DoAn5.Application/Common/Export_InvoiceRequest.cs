using DoAn5.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.Application.Common
{
    public class Export_InvoiceRequest
    {
        public Customer customer { get; set; }
        public List<Export_Invoice_Detail> list_detail { get; set; }
    }
}
