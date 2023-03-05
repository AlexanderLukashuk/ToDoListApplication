using System;
using System.Data.SqlClient;

namespace MyOwnToDoListApplication.Srvices
{
	public class SubToDoService
	{
		public SubToDoService()
		{
		}

		public void CreateSubToDo(string name, string desc, DateTime deadline, bool status, int todoId, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query =
                        "INSERT INTO SubToDo (Name, Description, Deadline, Status, ToDoId)" +
                        "VALUES (@name, @desc, @deadline, @status, @todoId);";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@desc", desc);
                        command.Parameters.AddWithValue("@deadline", deadline);
                        command.Parameters.AddWithValue("@status", false);
                        command.Parameters.AddWithValue("@todoId", todoId);

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

