using System;
namespace OsvitaBLL.Models
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

