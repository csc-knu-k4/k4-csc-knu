using System;
namespace OsvitaBLL.Models
{
	public class ChapterProgressDetailModel
	{
        public int Id { get; set; }
        public int StatisticId { get; set; }
        public List<int> ChapterId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}

