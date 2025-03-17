using System;
using OsvitaBLL.Models;

namespace OsvitaBLL.Interfaces
{
        public interface IStatisticReportService
        {
                Task<byte[]> GenerateAssignmetSetsReportAsync(int userId, int assignmentSetProgressDetailId);
                Task<byte[]> GenerateEducationClassAssignmetSetsReportAsync(int educationClassId, int assignmentSetId);
                Task<byte[]> GenerateDiagnosticalAssignmetSetsReportAsync(int educationClassId, int assignmentSetId);
        }
}

