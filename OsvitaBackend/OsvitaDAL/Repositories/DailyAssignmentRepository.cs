using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
    public class DailyAssignmentRepository : Repository<DailyAssignment>, IDailyAssignmentRepository
    {
        public DailyAssignmentRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<DailyAssignment>> GetDailyAssignmentsByUserIdWithDetailsAsync(int userId)
        {
            var dailyAssignments = await context.DailyAssignments
                .Include(x => x.AssignmentSet)
                .Where(x => x.UserId == userId).ToListAsync();
            return dailyAssignments;
        }
    }
}
