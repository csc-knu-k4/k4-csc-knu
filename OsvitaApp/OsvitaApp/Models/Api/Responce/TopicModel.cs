using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ChapterId { get; set; }
        public int OrderPosition { get; set; }
        public List<int> MaterialsIds { get; set; }
    }
}
