using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Interfaces
{
    public interface IAccountService
    {
        bool IsAuthenticated();
        Task<(bool IsSuccess, string ErrorMessage)> LoginAsync(string email, string password);
        void Logout();
    }
}
