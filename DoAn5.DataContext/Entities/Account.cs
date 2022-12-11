using DoAn5.DataContext.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public string Permissions { get; set; }
    }
}
