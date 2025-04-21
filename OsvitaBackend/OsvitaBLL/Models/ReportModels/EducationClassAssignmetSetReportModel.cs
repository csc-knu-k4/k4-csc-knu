using System;
namespace OsvitaBLL.Models.ReportModels
{
	public class EducationClassAssignmetSetReportModel
	{
        public int? AssignmentSetId { get; set; }
        public List<AssignmentSetReportModel> AssignmetSetReportModels { get; set; }
    }
}

