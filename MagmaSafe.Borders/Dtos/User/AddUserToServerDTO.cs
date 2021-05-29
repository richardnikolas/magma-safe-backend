namespace MagmaSafe.Borders.Dtos.User
{
    public class AddUserToServerDTO
    {
        public string UserEmail { get; set; }
        public string ServerId { get; set; }

        public AddUserToServerDTO(string userEmail, string serverId)
        {
            UserEmail = userEmail;
            ServerId = serverId;
        }
    }
}
