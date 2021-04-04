namespace MagmaSafe.Repositories.SQLStatements
{
    public class UserStatements
    {
        public const string GET_USER = @"SELECT * FROM User ";

        public const string CREATE_USER = @"
            INSERT INTO 
                User
            VALUES (
                @Id, @Name, @Email, @IsAdmin, @IsActive
            )
        ";
    }
}
