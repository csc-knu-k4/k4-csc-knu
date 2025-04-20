using System;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AreTopicIdsPresent(List<int> topicIds)
        {
            return await context.Topics
                .AnyAsync(x => topicIds.Contains(x.Id));
        }

    }
}

