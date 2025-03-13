using OsvitaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Interfaces
{
    public interface IChaptersService
    {
        Task<(bool IsSuccess, List<TopicModel> Data, string ErrorMessage)> GetTopicsAsync(int chapterId);
    }
}
