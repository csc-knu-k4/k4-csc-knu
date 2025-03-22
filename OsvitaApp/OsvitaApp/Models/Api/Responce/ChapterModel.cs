using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models
{
    public class ChapterModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public int OrderPosition { get; set; }

        public List<int> TopicsIds { get; set; }
    }
}
