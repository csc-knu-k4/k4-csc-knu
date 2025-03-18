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

        public async Task DeleteTopicPlanDetailByIdAsync(int id)
        {
            var topicPlanDetail = await context.TopicPlanDetails
                .FirstOrDefaultAsync(t => t.Id == id);
            if (topicPlanDetail is not null)
            {
                context.TopicPlanDetails.Remove(topicPlanDetail);
            }
        }

        public async Task<List<AssignmentSetPlanDetail>> GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(int id)
        {
            return await context.AssignmentSetPlanDetails.Where(x => x.EducationClassPlanId == id).ToListAsync();
        }

        public async Task<EducationClassPlan> GetEducationClassPlanByEducationClassIdWithDetailsAsync(int educationClassId)
        {
            return await context.EducationClassPlans
                .Include(x => x.AssignmentSetPlanDetails)
                .Include(x => x.TopicPlanDetails)
                .FirstOrDefaultAsync(x => x.EducationClassId == educationClassId);
        }

        public async Task<TopicPlanDetail> GetTopicPlanDetailByEducationClassPlanIdAndTopicIdAsync(int id, int topicId)
        {
            return await context.TopicPlanDetails
                .Where(x => x.EducationClassPlanId == id)
                .SingleOrDefaultAsync(x => x.TopicId == topicId);
        }

        public async Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationClassPlanIdAsync(int id)
        {
            return await context.TopicPlanDetails
               .Where(x => x.EducationClassPlanId == id).ToListAsync();
        }
    }
}

