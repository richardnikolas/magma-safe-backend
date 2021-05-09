﻿namespace MagmaSafe.Repositories.SQLStatements 
{
    public class ServerStatements 
    {
        public const string GET_SERVER = @"SELECT * FROM server ";

        public const string GET_SERVER_COUNT = @"SELECT COUNT(*) FROM Server ";

        public const string CREATE_SERVER = @"
            INSERT INTO 
                Server
            VALUES (
                @Id, @Name, @AdminId, @CreatedAt, @UpdatedAt
            )
        ";
    }
}
