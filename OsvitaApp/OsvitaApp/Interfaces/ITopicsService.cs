using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Interfaces;

public interface ITopicsService
{
    Task<(bool IsSuccess, List<MaterialModel> Data, string ErrorMessage)> GetMaterialsAsync(int topicID);
}