using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class AssignmentLinkRepository : Repository<AssignmentLink>, IAssignmentLinkRepository
    {
		public AssignmentLinkRepository(OsvitaDbContext context)
        : base(context)
        {
        }
    }
}

