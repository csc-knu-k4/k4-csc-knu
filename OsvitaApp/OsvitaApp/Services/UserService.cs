using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class UserService : BaseService, IUserService
    {
        private UserModel User { get; set; }
        private StatisticModel UserStatistics { get; set; }

        public UserService(ApiService apiService) : base(apiService, "api/users")
        {

        }

        public UserModel GetUser() => User;

        public StatisticModel GetStatistic() => UserStatistics;

        public async Task Init(int userId)
        {
            var getUserResult = await GetUserAsync(userId);
            if(getUserResult.IsSuccess)
            {
                User = getUserResult.Data;
            }
            else
            {
                throw new Exception(getUserResult.ErrorMessage);
            }

            var getUserStatistic = await GetUserStatisticAsync(userId);
            if(getUserStatistic.IsSuccess)
            {
                UserStatistics = getUserStatistic.Data;
            }
            else
            {
                throw new Exception(getUserStatistic.ErrorMessage);
            }

        }

        public async Task<(bool IsSuccess, UserModel Data, string ErrorMessage)> GetUserAsync(int userId)
        {
            return await _apiService.GetAsync<UserModel>(_serviceEndpoint + $"/{userId}");
        }

        public async Task<(bool IsSuccess, StatisticModel Data, string ErrorMessage)> GetUserStatisticAsync(int userId)
        {
            return await _apiService.GetAsync<StatisticModel>(_serviceEndpoint + $"/{userId}/statistic");
        }
    }
}
