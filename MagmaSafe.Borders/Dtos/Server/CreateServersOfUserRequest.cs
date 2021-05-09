namespace MagmaSafe.Borders.Dtos.Server
{
    public class CreateServersOfUserRequest
    {
        public string UserId { get; set; }
        public string ServerId { get; set; }

        public CreateServersOfUserRequest(string userId, string serverId)
        {
            UserId = userId;
            ServerId = serverId;
        }
    }
}
