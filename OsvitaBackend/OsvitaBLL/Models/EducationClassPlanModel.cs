using System;
namespace OsvitaBLL.Models
{
	public class EducationClassPlanModel
	{
		public int Id { get; set; }
        public int EducationClassId { get; set; }
        public List<AssignmentSetPlanDetailModel> AssignmentSetPlanDetails { get; set; }
    }
}

