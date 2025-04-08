using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Services;

public class AssignmentsService : BaseService, IAssignmentsService
{
    public AssignmentsService(ApiService apiService) : base(apiService, "api/assignments")
    {
    }
    
    public async Task<(bool IsSuccess, int Data, string ErrorMessage)> AddAssigmentSet(AssignmentSetModel assigmentModel)
    {
        return await _apiService.PostAsync<int>(_serviceEndpoint + $"/sets", assigmentModel);
    }
    
    public async Task<(bool IsSuccess, AssignmentSetModel Data, string ErrorMessage)> GetAssigmentSet(int assigmentId)
    {
        return await _apiService.GetAsync<AssignmentSetModel>(_serviceEndpoint + $"/sets/{assigmentId}");
    }
    
}