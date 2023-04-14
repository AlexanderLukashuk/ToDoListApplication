using System;
using MyOwnToDoListApplication.Interfaces;

namespace MyOwnToDoListApplication.Data
{
	public class PostgresDbInitializer : IDbInitializer
    {
		public PostgresDbInitializer()
		{
		}

        public void CreateTables()
        {
            throw new NotImplementedException();
        }

        public void DropTables()
        {
            throw new NotImplementedException();
        }
    }
}

