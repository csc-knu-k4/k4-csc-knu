using System;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaDAL.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
    {
		public UserRepository(OsvitaDbContext context)
        : base(context)
        {
        }
    }
}

