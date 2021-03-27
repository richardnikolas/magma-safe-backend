namespace MagmaSafe.Repositories.SQLStatements
{
    public class UserStatements
    {
        public const string GET_USER = @"SELECT * FROM User ";

        public const string CREATE_USER = @"
            INSERT INTO 
                User
            VALUES (
                @Id, @Name, @Email, @Password, @IsAdmin, @IsActive
            )
        ";

        public const string UPDATE_PASSWORD = @"
            UPDATE
                User
            SET 
                password = @NewPassword 
            WHERE 
                id = @Id
        ";
    }
}
