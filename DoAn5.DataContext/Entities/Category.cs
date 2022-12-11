using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
    }
}
