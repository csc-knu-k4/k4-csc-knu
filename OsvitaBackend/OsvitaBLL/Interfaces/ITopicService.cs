using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
    public interface ITopicService : ICrud<TopicModel>
    {
        Task<IEnumerable<TopicModel>> GetByChapterIdAsync(int chapterId);
        Task<IEnumerable<TopicModel>> GetByFilterAsync(FilterSearchModel filterSearchModel);
    }
}

