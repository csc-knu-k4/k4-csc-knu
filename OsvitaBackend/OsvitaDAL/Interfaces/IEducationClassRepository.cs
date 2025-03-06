using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IEducationClassRepository : IRepository<EducationClass>
    {
        Task<IEnumerable<EducationClass>> GetAllWithDetailsAsync();
        Task<EducationClass> GetByIdWithDetailsAsync(int id);
    }
}

