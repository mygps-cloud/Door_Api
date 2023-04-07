using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.UserModel
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
