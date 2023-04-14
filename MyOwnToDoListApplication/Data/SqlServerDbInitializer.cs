using System;
using System.Data.SqlClient;
using MyOwnToDoListApplication.Interfaces;

namespace MyOwnToDoListApplication.Data
{
	public class SqlServerDbInitializer : IDbInitializer
    {
        private readonly string connectionString;

        public SqlServerDbInitializer(string connectionString)
		{
            this.connectionString = connectionString;
        }

        public void CreateTables()
        {
            string query =
                "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tbl_name' and xtype='U')" +
                "CREATE TABLE ToDo (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name VARCHAR(50) NOT NULL," +
                "Progress FLOAT NOT NULL DEFAULT 0," +
                "UserId INT NOT NULL," +
                "CONSTRAINT FK_User_ToDo FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE);" +
                "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tbl_name' and xtype='U')" +
                "CREATE TABLE SubToDo (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Name VARCHAR(50) NOT NULL," +
                "Description VARCHAR(255) NOT NULL," +
                "Deadline DATETIME," +
                "Status BIT NOT NULL DEFAULT 0," +
                "ToDoId INT NOT NULL," +
                "CONSTRAINT FK_ToDo_SubToDo FOREIGN KEY (ToDoId) REFERENCES ToDo(Id) ON DELETE CASCADE);" +
                "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tbl_name' and xtype='U')" +
                "CREATE TABLE Users (" +
                "Id INT PRIMARY KEY IDENTITY(1,1)," +
                "Email VARCHAR(50) NOT NULL," +
                "UserPassword VARCHAR(20) NOT NULL);";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        public void DropTables()
        {
            throw new NotImplementedException();
        }
    }
}

