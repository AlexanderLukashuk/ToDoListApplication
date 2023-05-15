using System;
using System.Data.SqlClient;

namespace MyOwnToDoListApplication.Srvices
{
	public class UserService
	{
		public UserService()
		{
		}

		public void CreateUser(string email, string password, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query =
                        "INSERT INTO Users (Email, Password)" +
                        "VALUES (@email, @password);";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@email", email);
						command.Parameters.AddWithValue("@password", password);

						command.ExecuteNonQuery();
					}
                }
				catch(SqlException ex)
				{
					Console.WriteLine("ERROR: " + ex.Message);
				}
				finally
				{
					connection.Close();
				}
			}
		}

		public void DeleteUserByEmail(string email, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
                    connection.Open();

                    string query = "DELETE FROM User WHERE Email = @email";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@email", email);

						command.ExecuteNonQuery();
					}
                }
				catch (SqlException ex)
				{
                    Console.WriteLine("ERROR: " + ex.Message);
                }
				finally
				{
					connection.Close();
				}
            }
		}
	}
}

