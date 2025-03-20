using System;
using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
		public SubjectRepository(OsvitaDbContext context)
		: base(context)
		{
		}

        public async Task<IEnumerable<Subject>> GetAllWithDetailsAsync()
        {
            return await context.Subjects
                .Include(x => x.Chapters)
                .ThenInclude(x => x.Topics)
                .ThenInclude(x => x.Materials)
                .ToListAsync();
        }

        public async Task<Subject> GetByIdWithDetailsAsync(int id)
        {
            return await context.Subjects
                .Include(x => x.Chapters)
                .ThenInclude(x => x.Topics)
                .ThenInclude(x => x.Materials)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

