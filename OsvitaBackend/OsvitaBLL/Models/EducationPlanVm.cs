using System;
namespace OsvitaBLL.Models
{
	public class EducationPlanVm
	{
        public int Id { get; set; }
        public List<AssignmentSetVm> AssignmentSets { get; set; }
        public List<TopicVm> Topics { get; set; }
    }
}

