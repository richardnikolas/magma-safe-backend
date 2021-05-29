using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.ServersOfUsers
{
    public class UpdateIsFavoriteRequest
    {
        [Required]
        public string ServerId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool IsFavorite { get; set; }
    }
}
