using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Producer_Id { get; set; }
        public int Unit_Id { get; set; }
        public Status Status { get; set; }
        public DateTime Date_Created { get; set; }
    }
}
