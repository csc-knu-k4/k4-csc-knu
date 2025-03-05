using Microsoft.Maui.ApplicationModel.Communication;
using OsvitaApp.Interfaces;
using OsvitaApp.Models;
using OsvitaApp.Models.Api.Request;
using OsvitaApp.Models.Api.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class SubjectsService : BaseService, ISubjectsService
    {

        public SubjectsService(ApiService apiService) : base(apiService, "api/subjects")
        {
        }


        public async Task<(bool IsSuccess, List<SubjectModel> Data, string ErrorMessage)> GetSubjectsAsync()
        {
            return await _apiService.GetAsync<List<SubjectModel>>(_serviceEndpoint);
        }

        public async Task<(bool IsSuccess, List<ChapterModel> Data, string ErrorMessage)> GetChaptersAsync(int subjectID)
        {
            return await _apiService.GetAsync<List<ChapterModel>>(_serviceEndpoint + $"/{subjectID}/chapters");
        }
    }
}
