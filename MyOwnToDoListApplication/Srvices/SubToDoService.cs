using System;
using System.Data.SqlClient;
using MyOwnToDoListApplicationLibrary;

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

		public void DeleteSubToDoByName(string name, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "DELETE FROM SubToDo WHERE Name = @name";

					using (var command = new SqlCommand(query, connection))
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
					connection.Close();
				}
			}
		}

		public void UpdateSubToDoByName(string name, string newName, string newDesc, DateTime newDeadline, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "UPDATE SubToDo SET Name = @newName, Description = @newDesc, " +
						"Deadline = @newDeadline " +
						"WHERE Name = @name";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@newName", newName);
                        command.Parameters.AddWithValue("@newDesc", newDesc);
                        command.Parameters.AddWithValue("@newDeadline", newDeadline);
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

		public List<SubToDo> GetAllSubToDo(int todoID, string connectionString)
		{
			var subtodoList = new List<SubToDo>();

			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "SELECT * FROM SubToDo WHERE ToDoId = @todoID";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@todoID", todoID);

						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var subtodoID = reader.GetInt32(reader.GetOrdinal("Id"));
								var name = reader.GetString(reader.GetOrdinal("Name"));
								var desc = reader.GetString(reader.GetOrdinal("Description"));
								var deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
								var status = reader.GetBoolean(reader.GetOrdinal("Status"));
								var todoId = reader.GetInt32(reader.GetOrdinal("ToDoId"));

								subtodoList.Add(new SubToDo { Id = subtodoID, Name = name, Description = desc, Deadline = deadline, Status = status, ToDoId = todoId });
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

			return subtodoList;
		}

		public void ChangeSubToDoStatusByName(string name, bool newStatus, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "UPDATE SubToDo SET Status = @newStatus WHERE Name = @name";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@newStatus", newStatus);
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

		public SubToDo GetSubToDoByName(string name, string connectionString)
		{
			SubToDo subToDo = new SubToDo();

			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					string query = "SELECT * FROM SubToDo WHERE Name = @name";

					using (var command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@name", name);

						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var subToDoID = reader.GetInt32(reader.GetOrdinal("Id"));
								var subToDoName = reader.GetString(reader.GetOrdinal("Name"));
								var subToDoDesc = reader.GetString(reader.GetOrdinal("Description"));
								var subToDoDeadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
								var subToDoStatus = reader.GetBoolean(reader.GetOrdinal("Status"));
								var toDoID = reader.GetInt32(reader.GetOrdinal("ToDoId"));

								subToDo.Id = subToDoID;
                                subToDo.Name = subToDoName;
                                subToDo.Description = subToDoDesc;
                                subToDo.Deadline = subToDoDeadline;
                                subToDo.Status = subToDoStatus;
                                subToDo.ToDoId = toDoID;
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

			return subToDo;
		}
	}
}

