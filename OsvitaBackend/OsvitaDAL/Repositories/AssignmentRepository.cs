using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<Assignment>> GetAllWithDetailsAsync()
        {
            return await context.Assignments
                .Include(x => x.Answers)
                .ToListAsync();
        }

        public async Task<Assignment> GetByIdWithDetailsAsync(int id)
        {
            return await context.Assignments
                .Include(x => x.Answers)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
