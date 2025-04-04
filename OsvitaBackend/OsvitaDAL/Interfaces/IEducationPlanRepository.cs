using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface IEducationPlanRepository : IRepository<EducationPlan>
    {
        Task<EducationPlan> GetEducationPlanByIdWithDetailsAsync(int id);
        Task<EducationPlan> GetEducationPlanByUserIdAsync(int userId);
        Task<EducationPlan> GetEducationPlanByUserIdWithDetailsAsync(int userId);
        Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationPlanIdAsync(int id);
        Task<TopicPlanDetail> GetTopicPlanDetailByEducationPlanIdAndTopicIdAsync(int educationPlanId, int topicId);
        Task<AssignmentSetPlanDetail> GetAssignmentSetPlanDetailByEducationPlanIdAndAssignmentSetIdAsync(int educationPlanId, int assignmentSetId);
        Task DeleteTopicPlanDetailByIdAsync(int id);
        Task DeleteAssignmentSetPlanDetailByIdAsync(int id);
    }
}
