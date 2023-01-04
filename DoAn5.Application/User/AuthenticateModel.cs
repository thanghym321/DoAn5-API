using System.ComponentModel.DataAnnotations;

namespace DoAn5_API.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}