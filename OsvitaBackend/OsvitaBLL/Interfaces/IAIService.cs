using System;
using OsvitaBLL.Models.ReportModels;

namespace OsvitaBLL.Interfaces
{
	public interface IAIService
	{
        Task<string> GetRecomendationTextByDiagnosticalAssignmentSetResultAsync(AssignmentSetReportModel assignmentSetReportModel);
    }
}

