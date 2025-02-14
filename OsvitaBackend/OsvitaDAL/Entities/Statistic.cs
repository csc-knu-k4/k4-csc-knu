using System;
namespace OsvitaDAL.Entities
{
	public class Statistic : BaseEntity
	{
		public int UserId { get; set; }
		public List<ChapterProgressDetail> ChapterProgressDetails { get; set; }
	}
}

