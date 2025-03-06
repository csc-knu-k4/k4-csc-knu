using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IEducationClassService : ICrud<EducationClassModel>
    {
        Task DeleteStudentByIdAsync(int id, int educationClassId);
    }
}

