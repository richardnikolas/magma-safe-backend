namespace MagmaSafe.Repositories.SQLStatements
{
    public class ServersOfUsersStatements
    {
        public const string GET_SERVERS_OF_USERS = @"SELECT * FROM ServersOfUsers ";

        public const string GET_SERVERS_OF_USERS_COUNT = @"SELECT COUNT(*) FROM ServersOfUsers ";

        public const string CREATE_SERVERS_OF_USER = @"
            INSERT INTO 
                ServersOfUsers
            VALUES (
                @UserId, @ServerId, @IsFavorite
            )
        ";
        public const string GET_SERVERS_OF_USERS_SERVER_ID = @"
            SELECT 
                serverId
            FROM
                ServersOfUsers
            WHERE
                serverId = @ServerId
                AND userId = @UserId;
            ";

        public const string UPDATE_IS_FAVORITE = @"
            UPDATE
                ServersOfUsers
            SET 
                isFavorite = @IsFavorite
            WHERE
                ServerId = @ServerId AND UserId = @UserId
        ";
    }
}
