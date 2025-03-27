using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Services;

public class MaterialService : BaseService, IMaterialService
{
    public MaterialService(ApiService apiService) : base(apiService, "api/materials")
    {
    }
    
    public async Task<(bool IsSuccess, List<ContentBlockModel> Data, string ErrorMessage)> GetContentBlocksAsync(int materialID)
    {
        return await _apiService.GetAsync<List<ContentBlockModel>>(_serviceEndpoint + $"/{materialID}/contentblocks");
    }
}