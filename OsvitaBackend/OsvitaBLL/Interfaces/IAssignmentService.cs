using OsvitaBLL.Models;
namespace OsvitaBLL.Interfaces
{
    public interface IAssignmentService
    {
        Task<IEnumerable<AssignmentModel>> GetAllAssignmentsAsync();
        Task<IEnumerable<AssignmentModel>> GetAssignmentsByObjectIdAsync(int objectId, ObjectModelType objectModelType);
        Task<AssignmentModel> GetAssignmentByIdAsync(int id);
        Task<int> AddAssignmentAsync(AssignmentModel model);
        Task DeleteAssignmentByIdAsync(int id);
        Task UpdateAssignmentAsync(AssignmentModel model);
        Task<int> AddAssignmentSetAsync(AssignmentSetModel optionsModel);
        Task<AssignmentSetModel> GetAssignmentSetByIdAsync(int id);
    }
}
