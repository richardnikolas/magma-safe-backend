namespace MagmaSafe.Repositories.SQLStatements
{
    public class SecretStatements
    {
        public const string GET_SECRET = @"SELECT * FROM Secret ";

        public const string GET_SECRET_COUNT = @"SELECT COUNT(*) FROM Secret ";

        public const string CREATE_SECRET = @"
            INSERT INTO 
                Secret
            VALUES (
                @Id,
                @Name,
                @MagmaSecret,
                @UserId,
                @ServerId,
                @CreatedAt,
                @UpdatedAt,
                @LastAccessedByUser,
                @LastAccessed
            )
        ";

        public const string UPDATE_LAST_ACCESSED_BY_USER = @"
            UPDATE
                Secret
            SET
                lastAccessedByUser = @LastAccessedByUserId
            WHERE
                id = @Id
        ";
    }
}
