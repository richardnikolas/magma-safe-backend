using System.ComponentModel.DataAnnotations;

namespace MagmaSafe.Borders.Dtos.Server
{
    public class CreateServerRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AdminId { get; set; }
    }
}
