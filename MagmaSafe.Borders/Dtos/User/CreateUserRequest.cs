using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.User
{
    public class CreateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }
}
