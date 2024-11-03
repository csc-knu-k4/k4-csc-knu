using System;
namespace OsvitaBLL.Models
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

