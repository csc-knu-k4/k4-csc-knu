using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
    public interface IDailyAssignmentService
    {
        Task AddDailyAssignmentAsync(int userId);
        Task<AssignmentSetModel> GetDailyAssignmentSetAsync(int userId);
        Task<int> CountDailySetsToAdd(int userId);
    }
}
