namespace MagmaSafe.Repositories.SQLStatements
{
    public class SecretStatements
    {
        public const string GET_SECRET = @"SELECT * FROM Secret ";

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
                @UpdatedAt
            )
        ";
    }
}
