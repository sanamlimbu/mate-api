using System;
using OzMateApi.Entities;

namespace OzMateApi.Models
{
	public class DbService
	{
		protected readonly OzMateContext _context;
		public DbService(OzMateContext context)
		{
			_context = context;
		}
    }
}

