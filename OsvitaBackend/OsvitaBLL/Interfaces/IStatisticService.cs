using DocumentFormat.OpenXml.Office2010.Excel;
using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;

namespace OsvitaBLL.Interfaces
{
	public interface IStatisticService : ICrud<StatisticModel>
    {
        Task<StatisticModel> GetStatisticByUserIdAsync(int userId);
        Task<int> AddTopicProgressDetailAsync(TopicProgressDetailModel model, int userId);
        Task<int> UpdateTopicProgressDetailAsync(TopicProgressDetailModel model, int userId);
        Task<int> AddAssignmentSetProgressDetailAsync(AssignmentSetProgressDetailModel model, int userId);
        Task<int> UpdateAssignmentSetProgressDetailAsync(AssignmentSetProgressDetailModel model, int userId);
        Task<AssignmentSetProgressDetailModel> GetAssignmentSetProgressDetailAsync(int userId, int assignmentSetProgressDetailId);
        Task<bool> IsDailyAssignmentDoneAsync(int userId);
        Task<int> GetDailyAssignmentStreakAsync(int userId, DateTime? fromDate);
        Task<IEnumerable<UserDailyAssignmentRatingModel>> GetDailyAssignmentRatingAsync(IEnumerable<UserModel> students);
    }
}

