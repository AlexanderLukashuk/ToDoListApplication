using System;
using System.Data.SqlClient;
using MyOwnToDoListApplicationLibrary;

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

		public void DeleteToDoByName(string name, string connectionString)
		{
			using (var connecction = new SqlConnection(connectionString))
			{
				try
				{
					connecction.Open();

					string query = "DELETE FROM ToDo WHERE Name = @name";

					using (var command = new SqlCommand(query, connecction))
					{
						command.Parameters.AddWithValue("@name", name);

						command.ExecuteNonQuery();
					}
				}
				catch (SqlException ex)
				{
					Console.WriteLine("ERROR: " + ex.Message);
				}
				finally
				{
					connecction.Close();
				}
			}
		}

		public void UpdateToDoByName(string name, string newName, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "UPDATE ToDo SET Name = @newName WHERE Name = @name";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@newName", newName);
                        command.Parameters.AddWithValue("@name", name);

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

		public List<ToDo> GetAllToDo(string connectionString)
		{
			var todoList = new List<ToDo>();

			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "SELECT * FROM ToDo";

					using (var command = new SqlCommand(query, connection))
					{
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var todoID = reader.GetInt32(reader.GetOrdinal("Id"));
								var name = reader.GetString(reader.GetOrdinal("Name"));
								var progress = reader.GetDouble(reader.GetOrdinal("Progress"));

								todoList.Add(new ToDo { Id = todoID, Name = name, Progress = progress });
							}
						}
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

			return todoList;
		}
	}
}

