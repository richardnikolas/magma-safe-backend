using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.User
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
