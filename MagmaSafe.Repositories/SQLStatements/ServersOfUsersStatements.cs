namespace MagmaSafe.Repositories.SQLStatements
{
    public class ServersOfUsersStatements
    {
        public const string GET_SERVERS_OF_USERS_COUNT = @"SELECT COUNT(*) FROM ServersOfUsers ";

        public const string CREATE_SERVERS_OF_USER = @"
            INSERT INTO 
                ServersOfUsers
            VALUES (
                @UserId, @ServerId, @IsFavorite
            )
        ";
    }
}
