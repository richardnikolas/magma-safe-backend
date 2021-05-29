using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.Secret
{
    public class UpdateLastAccessedByUserRequest
    {
        [Required]
        public string SecretId { get; set; }
        [Required]
        public string LastAccessedByUserId { get; set; }
    }
}
