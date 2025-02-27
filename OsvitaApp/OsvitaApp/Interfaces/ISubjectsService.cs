using OsvitaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Interfaces
{
    public interface ISubjectsService
    {
        Task<(bool IsSuccess, List<SubjectModel> Data, string ErrorMessage)> GetSubjectsAsync();
    }
}
