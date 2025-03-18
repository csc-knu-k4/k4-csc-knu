namespace OsvitaDAL.Entities
{
	public class AssignmentSetPlanDetail : BaseEntity
	{
		public int? EducationClassPlanId { get; set; }
		public int AssignmentSetId { get; set; }
		public int? EducationPlanId { get; set; }
		public EducationClassPlan? EducationClassPlan { get; set; }
		// public AssignmentSet AssignmentSet { get; set; }
		public EducationPlan? EducationPlan { get; set; }
	}
}

