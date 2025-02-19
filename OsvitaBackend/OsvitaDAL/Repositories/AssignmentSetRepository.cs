using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class AssignmentSetRepository : Repository<AssignmentSet>, IAssignmentSetRepository
    {
		public AssignmentSetRepository(OsvitaDbContext context)
        : base(context)
        {
		}
	}
}

