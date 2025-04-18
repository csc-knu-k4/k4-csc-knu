using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IStatisticReportService statisticReportService;
        private readonly string mimeTypePdf = "application/pdf";
        private readonly string reportSuffix = "report.pdf";
        public ReportsController(IStatisticReportService statisticReportService)
        {
            this.statisticReportService = statisticReportService;
        }

        [HttpGet("userassignments")]
        public async Task<ActionResult> GetUserAssignmentsReport(int userId, int assignmentSetProgressDetailId)
        {
            var report = await statisticReportService.GenerateAssignmetSetsReportAsync(userId, assignmentSetProgressDetailId);
            if (report is not null)
            {
                return File(report, mimeTypePdf, $"statistic_{reportSuffix}");
            }
            return NotFound();
        }

        [HttpGet("userdiagnosticalassignments")]
        public async Task<ActionResult> GetUserDiagnosticalAssignmentsReport(int userId, int assignmentSetProgressDetailId)
        {
            var report = await statisticReportService.GenerateDiagnosticalAssignmetSetsReportAsync(userId, assignmentSetProgressDetailId);
            if (report is not null)
            {
                return File(report, mimeTypePdf, $"statistic_{reportSuffix}");
            }
            return NotFound();
        }

        [HttpGet("classassignments")]
        public async Task<ActionResult> GetEducationClassAssignmentsReport(int educationClassId, int assignmentSetId)
        {
            var report = await statisticReportService.GenerateEducationClassAssignmetSetsReportAsync(educationClassId, assignmentSetId);
            if (report is not null)
            {
                return File(report, mimeTypePdf, $"statistic_{reportSuffix}");
            }
            return NotFound();
        }

        [HttpGet("/ai/userassignments")]
        public async Task<ActionResult<string>> GetUserAssignmentsReportAI(int userId, int assignmentSetProgressDetailId)
        {
            var result = await statisticReportService.GenerateAssignmetSetsReportAIAsync(userId, assignmentSetProgressDetailId);
            if (result is not null)
            {
                return result;
            }
            return BadRequest();
        }
    }
}
