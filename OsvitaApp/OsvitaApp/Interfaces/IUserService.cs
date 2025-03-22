using OsvitaApp.Models.Api.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Interfaces
{
    public interface IUserService
    {
        StatisticModel GetStatistic();
        UserModel GetUser();
        Task<(bool IsSuccess, UserModel Data, string ErrorMessage)> GetUserAsync(int userId);
        Task<(bool IsSuccess, StatisticModel Data, string ErrorMessage)> GetUserStatisticAsync(int userId);
        Task Init(int userId);
    }
}
