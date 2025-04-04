using System;
namespace OsvitaBLL.Models
{
	public class AssignmentSetPlanDetailModel
	{
        public int Id { get; set; }
        public int? EducationClassPlanId { get; set; }
        public int AssignmentSetId { get; set; }
        public int? EducationPlanId { get; set; }
    }
}
