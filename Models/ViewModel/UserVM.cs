using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ViewModel
{
    public class UserVM
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
