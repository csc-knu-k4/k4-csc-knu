using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IEducationClassRepository : IRepository<EducationClass>
    {
        Task<IEnumerable<EducationClass>> GetAllWithDetailsAsync();
        Task<EducationClass> GetByIdWithDetailsAsync(int id);
        Task AddEducationClassInvitationAsync(EducationClassInvitation educationClassInvitation);
        Task<EducationClassInvitation> GetEducationClassInvitationByGuidAsync(string guid);
        Task DeleteEducationClassInvitationByGuidAsync(string guid);
    }
}

