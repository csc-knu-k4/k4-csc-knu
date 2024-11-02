using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class TopicRepository : Repository<Topic>, ITopicRepository
    {
		public TopicRepository(OsvitaDbContext context)
		: base(context)
		{
		}
	}
}

