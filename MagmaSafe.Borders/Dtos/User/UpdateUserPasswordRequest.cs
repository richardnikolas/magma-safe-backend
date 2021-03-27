using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.User
{
    public class UpdateUserPasswordRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
