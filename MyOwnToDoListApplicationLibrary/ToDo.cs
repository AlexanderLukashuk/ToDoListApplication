using System;
namespace MyOwnToDoListApplicationLibrary
{
	public class ToDo
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Progress { get; set; }

		public ToDo()
		{
		}
	}
}

