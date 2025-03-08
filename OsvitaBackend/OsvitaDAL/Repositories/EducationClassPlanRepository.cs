using System;
using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class EducationClassPlanRepository : Repository<EducationClassPlan>, IEducationClassPlanRepository
    {
		public EducationClassPlanRepository(OsvitaDbContext context)
        : base(context)
        {
        }

        public async Task<List<AssignmentSetPlanDetail>> GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(int id)
        {
            return await context.AssignmentSetPlanDetails.Where(x => x.EducationClassPlanId == id).ToListAsync();
        }

        public async Task<EducationClassPlan> GetEducationClassPlanByEducationClassIdWithDetailsAsync(int educationClassId)
        {
            return await context.EducationClassPlans
                .Include(x => x.AssignmentSetPlanDetails)
                .FirstOrDefaultAsync(x => x.EducationClassId == educationClassId);
        }
    }
}

