using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IEducationClassService : ICrud<EducationClassModel>
    {
        Task DeleteStudentByIdAsync(int id, int educationClassId);
        Task InviteStudentByEmailAsync(string email, int educationClassId);
        Task ConfirmStudentAsync(int id, int educationClassId, string guid);
    }
}

