using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
    public interface IEducationPlanService : ICrud<EducationPlanModel>
    {
        Task<EducationPlanModel> GetEducationPlanByUserIdAsync(int userId);
        Task<TopicPlanDetailModel> GetTopicPlanDetailAsync(int userId, int topicId);
        Task<int> AddTopicPlanDetailAsync(TopicPlanDetailModel model, int userId);
        Task<int> AddAssignmentSetPlanDetailAsync(AssignmentSetPlanDetailModel model, int userId);
        Task<int> UpdateTopicPlanDetailAsync(TopicPlanDetailModel model, int userId);
        Task<int> DeleteTopicPlanDetailAsync(int userId, int topicId);
    }
}
