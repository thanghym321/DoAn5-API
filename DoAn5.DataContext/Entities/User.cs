using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public Status Status { get; set; }
    }
}
