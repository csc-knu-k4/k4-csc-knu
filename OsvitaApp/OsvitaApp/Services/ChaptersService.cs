using OsvitaApp.Interfaces;
using OsvitaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class ChaptersService : BaseService, IChaptersService
    {
        public ChaptersService(ApiService apiService) : base(apiService, "api/chapters")
        {
        }

        public async Task<(bool IsSuccess, List<TopicModel> Data, string ErrorMessage)> GetTopicsAsync(int chapterId)
        {
            return await _apiService.GetAsync<List<TopicModel>>(_serviceEndpoint + $"/{chapterId}/topics");
        }

    }
}
