using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
		public SubjectRepository(OsvitaDbContext context)
		: base(context)
		{
		}
	}
}

