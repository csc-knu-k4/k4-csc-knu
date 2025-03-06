using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
    public interface IEducationPlanService : ICrud<EducationPlanModel>
    {
        Task<EducationPlanModel> GetEducationPlanByUserIdAsync(int userId);
        Task<TopicPlanDetailModel> GetTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId);
        Task<int> AddTopicPlanDetailAsync(TopicPlanDetailModel model, int userId);
        Task<int> UpdateTopicPlanDetailAsync(TopicPlanDetailModel model, int userId, int topicId);
        Task  DeleteTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId);
    }
}
