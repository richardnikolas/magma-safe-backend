using System;

namespace MagmaSafe.Borders.Entities
{
    public class Server
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
