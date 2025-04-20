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
            var count = await context.Topics
                .CountAsync(x => topicIds.Contains(x.Id));

            return count == topicIds.Count;
        }

    }
}

