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

        public async Task<TopicPlanDetail> DeleteTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId)
        {
            var educationPlan =  await context.EducationPlans
                .Include(x => x.TopicPlanDetails)
                .SingleOrDefaultAsync(x => x.UserId == userId);
            var topicPlanDetail = educationPlan.TopicPlanDetails.Find(x => x.TopicId == topicId);

            if (topicPlanDetail is not null)
            {
                context.TopicPlanDetails.Remove(topicPlanDetail);
            }
            return topicPlanDetail;
        }

        public async Task<EducationPlan> GetEducationPlanByIdWithDetailsAsync(int id)
        {
            return await context.EducationPlans
                .Include(x => x.TopicPlanDetails)
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
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationPlanIdAsync(int id)
        {
            return await context.TopicPlanDetails.Where(x => x.EducationPlanId == id).ToListAsync();
        }
    }
}
