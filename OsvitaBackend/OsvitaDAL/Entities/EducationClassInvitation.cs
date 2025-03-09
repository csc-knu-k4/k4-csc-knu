using System;
namespace OsvitaDAL.Entities
{
	public class EducationClassInvitation : BaseEntity
	{
		public string Guid { get; set; }
		public int EducationClassId { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}

