using DoAn5.DataContext.Entities;
using DoAn5_API.Models;

namespace DoAn5_API.Entities
{
    public class UserModel
    {
        public User user { get; set; }
        public Account account { get; set; }
    }
}
