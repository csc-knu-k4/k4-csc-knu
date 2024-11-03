using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class ContentBlockRepository : Repository<ContentBlock>, IContentBlockRepository
    {
		public ContentBlockRepository(OsvitaDbContext context)
		: base(context)
		{
		}
	}
}

