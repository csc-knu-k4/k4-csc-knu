using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface IDailyAssignmentRepository : IRepository<DailyAssignment>
    {
        Task<IEnumerable<DailyAssignment>> GetDailyAssignmentsByUserIdWithDetailsAsync(int userId);

    }
}
