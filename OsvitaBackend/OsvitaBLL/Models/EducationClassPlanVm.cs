using System;
namespace OsvitaBLL.Models
{
	public class EducationClassPlanVm
	{
		public int Id { get; set; }
		public int EducationClassId { get; set; }
        public string EducationClassName { get; set; }
        public List<AssignmentSetVm> AssignmentSets { get; set; }
        public List<TopicVm> Topics { get; set; }
    }
}

