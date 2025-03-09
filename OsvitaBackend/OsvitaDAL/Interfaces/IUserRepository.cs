using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IUserRepository : IRepository<User>
    {
		Task<User> GetByIdWithDetailsAsync(int id);
	}
}

