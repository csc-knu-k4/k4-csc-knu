using System;
namespace OsvitaDAL.Entities
{
	public class TopicProgressDetail : BaseEntity
	{
		public int StatisticId { get; set; }
		public int TopicId { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime CompletedDate { get; set; }
		public Topic Topic { get; set; }
		public Statistic Statistic { get; set; }
	}
}

