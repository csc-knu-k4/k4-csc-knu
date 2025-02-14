using System;
namespace OsvitaDAL.Entities
{
	public class ChapterProgressDetail : BaseEntity
	{
		public int StatisticId { get; set; }
		public int ChapterId { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime CompletedDate { get; set; }
		public Chapter Chapter { get; set; }
		public Statistic Statistic { get; set; }
	}
}

