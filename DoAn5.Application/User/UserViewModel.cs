using DoAn5.DataContext.Enums;
using System;

namespace DoAn5_API.Entities
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status_Account { get; set; }
        public string Role { get; set; }
        public int? User_Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string GioiTinh { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Status Status_User { get; set; }
        public string Token { get; set; }
    }
}
