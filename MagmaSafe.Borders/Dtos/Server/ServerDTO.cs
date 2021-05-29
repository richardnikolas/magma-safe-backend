namespace MagmaSafe.Borders.Dtos.Server
{
    public class ServerDTO
    {
        public Entities.Server Server { get; set; }
        public bool IsFavorite { get; set; }
        public int SecretsCount { get; set; }
        public int UsersCount { get; set; }
        public string AdminName { get; set; }

        public ServerDTO(Entities.Server server, bool isFavorite, int secretsCount, int usersCount, string adminName)
        {
            Server = server;
            IsFavorite = isFavorite;
            SecretsCount = secretsCount;
            UsersCount = usersCount;
            AdminName = adminName;
        }
    }
}

