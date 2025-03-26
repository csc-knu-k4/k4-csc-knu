using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Services;

public class TopicsService : BaseService, ITopicsService
{
    public TopicsService(ApiService apiService) : base(apiService, "api/topics")
    {
    }
    
    public async Task<(bool IsSuccess, List<MaterialModel> Data, string ErrorMessage)> GetMaterialsAsync(int topicID)
    {
        return await _apiService.GetAsync<List<MaterialModel>>(_serviceEndpoint + $"/{topicID}/materials");
    }
    
}