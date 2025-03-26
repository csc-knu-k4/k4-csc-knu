using OsvitaApp.Interfaces;

namespace OsvitaApp.Services;

public class MaterialService : BaseService, IMaterialService
{
    public MaterialService(ApiService apiService) : base(apiService, "api/materials")
    {
    }
}