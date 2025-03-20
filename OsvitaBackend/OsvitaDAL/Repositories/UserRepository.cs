using System;
using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
    {
		public UserRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await context.Users
                .Include(x => x.Statistic)
                .Include(x => x.EducationClasses)
                .Include(x => x.EducationPlan)
                .Include(x => x.RecomendationMessages)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

