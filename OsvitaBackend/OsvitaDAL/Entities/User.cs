using System;
namespace OsvitaDAL.Entities
{
	public class User : BaseEntity
	{
		public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? SecondName { get; set; }
		public Statistic Statistic { get; set; }
		public List<EducationClass> EducationClasses { get; set; }
	}
}

