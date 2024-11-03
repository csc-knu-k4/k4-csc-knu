using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IContentBlockService : ICrud<ContentBlockModel>
    {
        Task<IEnumerable<ContentBlockModel>> GetByMaterialIdAsync(int materialId);
        Task<IEnumerable<ContentBlockModel>> GetByFilterAsync(FilterSearchModel filterSearchModel);
    }
}

