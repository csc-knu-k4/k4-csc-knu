using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class EducationClassRepository : Repository<EducationClass>, IEducationClassRepository
    {
		public EducationClassRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<EducationClass>> GetAllWithDetailsAsync()
        {
            return await context.EducationClasses
                .Include(x => x.Students)
                .Include(x => x.Teacher)
                .ToListAsync();
        }

        public async Task<EducationClass> GetByIdWithDetailsAsync(int id)
        {
            return await context.EducationClasses
                .Include(x => x.Students)
                .Include(x => x.Teacher)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddEducationClassInvitationAsync(EducationClassInvitation educationClassInvitation)
        {
            await context.EducationClassInvitations.AddAsync(educationClassInvitation);
        }

        public async Task<EducationClassInvitation> GetEducationClassInvitationByGuidAsync(string guid)
        {
            return await context.EducationClassInvitations.FirstOrDefaultAsync(x => x.Guid == guid);
        }

        public async Task DeleteEducationClassInvitationByGuidAsync(string guid)
        {
            var educationClassInvitation = await context.EducationClassInvitations.FirstOrDefaultAsync(x => x.Guid == guid);
            if (educationClassInvitation is not null)
            {
                context.EducationClassInvitations.Remove(educationClassInvitation);
            }
        }
    }
}

