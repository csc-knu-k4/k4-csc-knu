using System;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
	{
        Task<bool> AreTopicIdsPresent(List<int> topicIds);

    }
}

