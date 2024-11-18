using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
		public MaterialRepository(OsvitaDbContext context)
		: base(context)
		{
		}

        public async Task<IEnumerable<Material>> GetAllWithDetailsAsync()
        {
            return await context.Materials
                .Include(x => x.ContentBlocks)
                .Include(x => x.Topic)
                .ToListAsync();
        }

        public async Task<Material> GetByIdWithDetailsAsync(int id)
        {
            return await context.Materials
                .Include(x => x.ContentBlocks)
                .Include(x => x.Topic)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}

