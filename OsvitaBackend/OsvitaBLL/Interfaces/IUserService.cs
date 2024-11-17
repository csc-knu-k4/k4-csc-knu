using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
	public interface IUserService : ICrud<UserModel>
    {
        Task<UserModel> GetByEmailAsync(string email);
	}
}

