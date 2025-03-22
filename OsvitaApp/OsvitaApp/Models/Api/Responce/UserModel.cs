using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.Api.Responce
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public int? StatisticModelId { get; set; }
        public List<int> EducationClassesIds { get; set; }
        public List<string> Roles { get; set; }
    }
}
