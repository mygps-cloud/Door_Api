using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.UserModel
{
    public class UserDtoModel
    {
	    [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
