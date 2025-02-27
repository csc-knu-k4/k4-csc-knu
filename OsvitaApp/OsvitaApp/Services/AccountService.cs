using OsvitaApp.Interfaces;
using OsvitaApp.Models.Api.Request;
using OsvitaApp.Models.Api.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private string _token;

        public AccountService(ApiService apiService) : base(apiService, "api/account")
        {
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> LoginAsync(string email, string password)
        {
            var payload = new AuthRequest() { Email = email, Password = password };
            var response = await _apiService.PostAsync<AuthResponse>(_serviceEndpoint + "/Login", payload);

            if(response.IsSuccess)
            {
                _token = response.Data.Token;
                _apiService.SetAuthToken(_token);
                return (true, string.Empty);
            }

            return (false, response.ErrorMessage);
        }

        public bool IsAuthenticated() => !string.IsNullOrEmpty(_token);

        public void Logout()
        {
            _token = string.Empty;
            _apiService.ClearAuthToken();
        }
    }
}
