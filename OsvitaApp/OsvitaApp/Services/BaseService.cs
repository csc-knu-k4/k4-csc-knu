using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class BaseService
    {

        protected ApiService _apiService;
        protected string _serviceEndpoint;

        protected BaseService(ApiService apiService, string serviceEndpoint)
        {
            _apiService = apiService;
            _serviceEndpoint = serviceEndpoint;
        }

    }
}
