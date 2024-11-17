using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<IEnumerable<Assignment>> GetAllWithDetailsAsync();
        Task<Assignment> GetByIdWithDetailsAsync(int id);
    }
}
