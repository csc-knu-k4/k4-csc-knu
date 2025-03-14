using System;
namespace OsvitaBLL.Models
{
	public class SubjectModel
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public List<int> ChaptersIds { get; set; }
        public List<ChapterModel> Chapters { get; set; }
    }
}

