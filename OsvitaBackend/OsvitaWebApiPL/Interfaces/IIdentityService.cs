using System;
using OsvitaBLL.Models;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Interfaces
{
	public interface IIdentityService
	{
        Task RegisterUser(UserDTO userDto);
        Task<string> LoginUser(UserLoginDTO userLoginDto);
    }
}

