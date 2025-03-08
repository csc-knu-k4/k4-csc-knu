using System;
namespace OsvitaBLL.Interfaces
{
	public interface IStatisticReportService
	{
        Task<byte[]> GenerateAssignmetSetsReportAsync(int userId, int assignmentSetProgressDetailId);
        Task<byte[]> GenerateEducationClassAssignmetSetsReportAsync(int educationClassId, int assignmentSetId);
    }
}

