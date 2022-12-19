using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.Application.Common
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public string Category_Name { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string[] Image_Detail { get; set; }
        public int Producer_Id { get; set; }
        public string Producer_Name { get; set; }
        public int Unit_Id { get; set; }
        public string Unit_Name { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}
