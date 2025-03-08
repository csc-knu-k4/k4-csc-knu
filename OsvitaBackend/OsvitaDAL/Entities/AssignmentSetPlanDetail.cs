using System;
namespace OsvitaDAL.Entities
{
	public class AssignmentSetPlanDetail : BaseEntity
	{
		public int? EducationClassPlanId { get; set; }
		public int AssignmentSetId { get; set; }
	}
}

