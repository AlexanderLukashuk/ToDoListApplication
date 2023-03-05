using System;
using System.Data.SqlClient;

namespace MyOwnToDoListApplication.Srvices
{
	public class ToDoService
	{
		public ToDoService()
		{
		}

		public void CreateToDo(string name, double progress, string connectionString)
		{
			using (var connecction = new SqlConnection(connectionString))
			{
				try
				{
					connecction.Open();

					string query =
                        "INSERT INTO ToDo (Name, Progress)" +
                        "VALUES (@name, @progress);";

					using (var command = new SqlCommand(query, connecction))
					{
						command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@progress", progress);

						command.ExecuteNonQuery();
                    }
                }
				catch (SqlException ex)
				{
					Console.WriteLine("ERROR: Can't connect to service " + ex.Message);
				}
				finally
				{
					connecction.Close();
				}
            }
		}
	}
}

