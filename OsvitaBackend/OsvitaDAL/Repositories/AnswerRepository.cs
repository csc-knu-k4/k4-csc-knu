using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
		public AnswerRepository(OsvitaDbContext context)
        : base(context)
        {
        }
    }
}

