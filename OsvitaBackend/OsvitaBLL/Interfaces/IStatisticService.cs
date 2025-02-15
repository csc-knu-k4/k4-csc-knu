using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IStatisticService : ICrud<StatisticModel>
    {
        Task<StatisticModel> GetStatisticByUserIdAsync(int userId);
    }
}

