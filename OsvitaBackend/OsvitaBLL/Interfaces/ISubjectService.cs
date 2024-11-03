using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface ISubjectService : ICrud<SubjectModel>
	{
        Task<IEnumerable<SubjectModel>> GetByFilterAsync(FilterSearchModel filterSearchModel);
    }
}

