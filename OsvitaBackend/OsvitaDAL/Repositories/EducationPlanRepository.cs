using Microsoft.EntityFrameworkCore;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
    public class EducationPlanRepository : Repository<EducationPlan>, IEducationPlanRepository
    {
        public EducationPlanRepository(OsvitaDbContext context)
            : base(context)
        {
        }

        public async Task DeleteAssignmentSetPlanDetailByIdAsync(int id)
        {
            var assignmentSetPlanDetail = await context.AssignmentSetPlanDetails
                .FirstOrDefaultAsync(t => t.Id == id);
            context.AssignmentSetPlanDetails.Remove(assignmentSetPlanDetail);
        }

        public async Task DeleteTopicPlanDetailByIdAsync(int id)
        {
            var topicPlanDetail =  await context.TopicPlanDetails
                .FirstOrDefaultAsync(t => t.Id == id);
            context.TopicPlanDetails.Remove(topicPlanDetail);
        }

        public async Task<AssignmentSetPlanDetail> GetAssignmentSetPlanDetailByEducationPlanIdAndAssignmentSetIdAsync(int educationPlanId, int assignmentSetId)
        {
            return await context.AssignmentSetPlanDetails
                .Where(x => x.EducationPlanId == educationPlanId)
                .SingleOrDefaultAsync(x => x.AssignmentSetId == assignmentSetId);
        }

        public async Task<EducationPlan> GetEducationPlanByIdWithDetailsAsync(int id)
        {
            return await context.EducationPlans
                .Include(x => x.TopicPlanDetails)
                .Include(x => x.AssignmentSetPlanDetails)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EducationPlan> GetEducationPlanByUserIdAsync(int userId)
        {
            return await context.EducationPlans.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<EducationPlan> GetEducationPlanByUserIdWithDetailsAsync(int userId)
        {
            return await context.EducationPlans
                .Include(x => x.TopicPlanDetails)
                .ThenInclude(x => x.Topic)
                .Include(x => x.AssignmentSetPlanDetails)
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<TopicPlanDetail> GetTopicPlanDetailByEducationPlanIdAndTopicIdAsync(int educationPlanId, int topicId)
        {
            return await context.TopicPlanDetails
                .Where(x => x.EducationPlanId == educationPlanId)
                .SingleOrDefaultAsync(x => x.TopicId == topicId);
        }

        public async Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationPlanIdAsync(int id)
        {
            return await context.TopicPlanDetails
                .Where(x => x.EducationPlan.Id == id).ToListAsync();
        }
    }
}
