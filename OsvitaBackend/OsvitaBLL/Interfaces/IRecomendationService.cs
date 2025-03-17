using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IRecomendationService
	{
		Task AddRecomendationMessageAsync(int userId);
		Task<IEnumerable<RecomendationMessageModel>> GetRecomendationMessagesByUserIdAsync(int userId);
		Task<RecomendationMessageModel> GetRecomendationMessageByIdAsync(int id);
		Task DeleteRecomendationMessageByIdAsync(int id);
		Task UpdateRecomendationMessageAsync(RecomendationMessageModel model);
    }
}

