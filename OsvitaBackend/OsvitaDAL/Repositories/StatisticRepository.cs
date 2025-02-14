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

        public async Task<Statistic> GetStatisticByIdWithDetailsAsync(int id)
        {
            return await context.Statistics
                .Include(x => x.ChapterProgressDetails)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Statistic> GetStatisticByUserIdAsync(int userId)
        {
            return await context.Statistics.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Statistic> GetStatisticByUserIdWithDetailsAsync(int userId)
        {
            return await context.Statistics
                .Include(x => x.ChapterProgressDetails)
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }
    }
}

