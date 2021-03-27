using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.Secret
{
    public class CreateSecretRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MagmaSecret { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ServerId { get; set; }
    }
}
