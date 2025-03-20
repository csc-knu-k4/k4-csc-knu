using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IStaticFileService staticFileService;
        private readonly IExcelService excelService;
        public FilesController(IStaticFileService staticFileService, IExcelService excelService)
        {
            this.staticFileService = staticFileService;
            this.excelService = excelService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file, [FromQuery] bool addCustomGuid)
        {
            try
            {
                var result = await staticFileService.Upload(file, addCustomGuid);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/materials")]
        public async Task<ActionResult> Import(IFormFile file)
        {
            try
            {
                await excelService.ImportAsync(file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
