using System;
namespace OsvitaDAL.Entities
{
	public class User : BaseEntity
	{
		public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? SecondName { get; set; }
		public int? StatisticId { get; set; }
		public Statistic Statistic { get; set; }
	}
}

