using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
		public ChapterRepository(OsvitaDbContext context)
		: base(context)
		{
		}
	}
}

