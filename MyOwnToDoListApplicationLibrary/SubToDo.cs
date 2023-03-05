using System;
namespace MyOwnToDoListApplicationLibrary
{
	public class SubToDo
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime Deadline { get; set; }
		public bool Status { get; set; }
		public int ToDoId { get; set; }

		public SubToDo()
		{
		}
	}
}

