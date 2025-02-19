using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class StatisticRepository : Repository<Statistic>, IStatisticRepository
	{
        public StatisticRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<List<TopicProgressDetail>> GetTopicProgressDetailsByStatisticIdAsync(int id)
        {
            return await context.TopicProgressDetails.Where(x => x.StatisticId == id).ToListAsync();
        }

        public async Task<List<AssignmentSetProgressDetail>> GetAssignmentSetProgressDetailsByStatisticIdAsync(int id)
        {
            return await context.AssignmentSetProgressDetails
                .Include(x => x.AssignmentProgressDetails)
                .Where(x => x.StatisticId == id).ToListAsync();
        }

        public async Task<Statistic> GetStatisticByIdWithDetailsAsync(int id)
        {
            return await context.Statistics
                .Include(x => x.TopicProgressDetails)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Statistic> GetStatisticByUserIdAsync(int userId)
        {
            return await context.Statistics.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Statistic> GetStatisticByUserIdWithDetailsAsync(int userId)
        {
            return await context.Statistics
                .Include(x => x.TopicProgressDetails)
                .Include(x => x.AssignmentSetProgressDetails).ThenInclude(x => x.AssignmentProgressDetails)
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }
    }
}

