using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IChapterService : ICrud<ChapterModel>
    {
        Task<IEnumerable<ChapterModel>> GetBySubjectIdAsync(int subjectId);
        Task<IEnumerable<ChapterModel>> GetByFilterAsync(FilterSearchModel filterSearchModel);
    }
}

