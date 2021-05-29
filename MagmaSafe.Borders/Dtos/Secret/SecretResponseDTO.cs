using System;

namespace MagmaSafe.Borders.Dtos.Secret
{
    public class SecretResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MagmaSecret { get; set; }
        public string ServerName { get; set; }
        public string LastAccessedByUser { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public SecretResponseDTO
            (
                string id, string name, string magmaSecret, string serverName, 
                string lastAccessedByUser, DateTime lastAccessed, DateTime createdAt, DateTime updatedAt
            )
        {
            Id = id;
            Name = name;
            MagmaSecret = magmaSecret;
            ServerName = serverName;
            LastAccessedByUser = lastAccessedByUser;
            LastAccessed = lastAccessed;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
