using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IMaterialService : ICrud<MaterialModel>
    {
        Task<IEnumerable<MaterialModel>> GetByTopicIdAsync(int topicId);
        Task<IEnumerable<MaterialModel>> GetByFilterAsync(FilterSearchModel filterSearchModel);
    }
}

