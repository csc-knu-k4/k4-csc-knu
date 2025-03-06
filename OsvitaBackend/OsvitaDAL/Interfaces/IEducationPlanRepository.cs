using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface IEducationPlanRepository : IRepository<EducationPlan>
    {
        Task<EducationPlan> GetEducationPlanByIdWithDetailsAsync(int id);
        Task<EducationPlan> GetEducationPlanByUserIdAsync(int userId);
        Task<EducationPlan> GetEducationPlanByUserIdWithDetailsAsync(int userId);
        Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationPlanIdAsync(int id);
        Task<TopicPlanDetail> DeleteTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId);
    }
}
