using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> ChaptersIds { get; set; }
        public bool IsEnabled { get { return !ChaptersIds?.Any() ?? false; } private set { } }

    }

}
