using OsvitaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Services
{
    public class ChaptersService : BaseService, IChaptersService
    {
        public ChaptersService(ApiService apiService) : base(apiService, "api/chapters")
        {
        }
    }
}
