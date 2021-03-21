using System;

namespace MagmaSafe.Borders.Entities
{
    public class Secret
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MagmaSecret { get; set; }
        public string UserId { get; set; }
        public string ServerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
