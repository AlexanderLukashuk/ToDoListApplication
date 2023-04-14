using System;
namespace MyOwnToDoListApplication.Interfaces
{
	public interface IDbInitializer
	{
		public void CreateTables();

		public void DropTables();
	}
}

