using System;
namespace OsvitaDAL.Entities
{
	public class EducationClassPlan : BaseEntity
	{
		public int EducationClassId { get; set; }
		public List<AssignmentSetPlanDetail> AssignmentSetPlanDetails { get; set; }
	}
}

