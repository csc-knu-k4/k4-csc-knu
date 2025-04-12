using System;
using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;

namespace OsvitaBLL.Interfaces
{
    public interface IStatisticReportService
    {
        Task<byte[]> GenerateAssignmetSetsReportAsync(int userId, int assignmentSetProgressDetailId);
        Task<byte[]> GenerateEducationClassAssignmetSetsReportAsync(int educationClassId, int assignmentSetId);
        Task<byte[]> GenerateDiagnosticalAssignmetSetsReportAsync(int educationClassId, int assignmentSetId);
        Task<List<AssignmentSetReportModel>> GetLastAssignmetSetsReportsAsync(int userId, int assignmentSetsCount);
        Task<AssignmentSetReportModel> GetAssignmetSetReportModelAsync(int userId, int assignmentSetProgressDetailId);
        Task<EducationClassAssignmetSetReportModel> GetEducationClassAssignmetSetReportModelAsync(int educationClassId, int assignmentSetId);
    }
}