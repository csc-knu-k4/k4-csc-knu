using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Interfaces
{
	public interface IIdentityService
	{
        Task RegisterUser(UserDTO userDto);
        Task<string> LoginUser(UserLoginDTO userLoginDto);
        Task<List<string>> GetUserRoles(string email);
    }
}

