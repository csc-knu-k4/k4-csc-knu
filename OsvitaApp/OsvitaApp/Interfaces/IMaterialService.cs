using OsvitaApp.Models.Api.Responce;

namespace OsvitaApp.Interfaces;

public interface IMaterialService
{
    Task<(bool IsSuccess, List<ContentBlockModel> Data, string ErrorMessage)> GetContentBlocksAsync(int materialID);
}