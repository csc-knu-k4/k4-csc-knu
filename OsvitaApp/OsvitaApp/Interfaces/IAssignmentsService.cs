using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Interfaces;

public interface IAssignmentsService
{
    Task<(bool IsSuccess, int Data, string ErrorMessage)> AddAssigmentSet(AssignmentSetModel assigmentModel);
    Task<(bool IsSuccess, AssignmentSetModel Data, string ErrorMessage)> GetAssigmentSet(int assigmentId);
}