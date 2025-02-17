using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IStatisticService : ICrud<StatisticModel>
    {
        Task<StatisticModel> GetStatisticByUserIdAsync(int userId);
        Task<int> AddTopicProgressDetailAsync(TopicProgressDetailModel model, int userId);
        Task<int> UpdateTopicProgressDetailAsync(TopicProgressDetailModel model, int userId);
        Task<int> AddAssignmentSetProgressDetailsAsync(IEnumerable<AssignmentSetProgressDetailModel> models, int userId);
        Task<int> UpdateAssignmentSetProgressDetailsAsync(IEnumerable<AssignmentSetProgressDetailModel> models, int userId);
    }
}

