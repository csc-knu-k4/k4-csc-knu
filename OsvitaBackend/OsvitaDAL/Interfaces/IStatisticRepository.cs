using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IStatisticRepository : IRepository<Statistic>
    {
        Task<Statistic> GetStatisticByIdWithDetailsAsync(int id);
        Task<Statistic> GetStatisticByUserIdAsync(int userId);
        Task<Statistic> GetStatisticByUserIdWithDetailsAsync(int userId);
        Task<List<TopicProgressDetail>> GetTopicProgressDetailsByStatisticIdAsync(int id);
        Task<List<AssignmentSetProgressDetail>> GetAssignmentSetProgressDetailsByStatisticIdAsync(int id);
    }
}

