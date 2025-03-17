using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class RecomendationMessageRepository : Repository<RecomendationMessage>, IRecomendationMessageRepository
	{
		public RecomendationMessageRepository(OsvitaDbContext context)
        : base(context)
        {
        }
	}
}

