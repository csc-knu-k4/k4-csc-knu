using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.Api.Responce
{
    public class StatisticModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<TopicProgressDetailModel> TopicProgressDetails { get; set; }
        public List<AssignmentSetProgressDetailModel> AssignmentSetProgressDetails { get; set; }
    }
}
