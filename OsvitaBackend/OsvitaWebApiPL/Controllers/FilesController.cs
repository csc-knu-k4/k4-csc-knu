using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IStaticFileService staticFileService;
        public FilesController(IStaticFileService staticFileService)
        {
            this.staticFileService = staticFileService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            try
            {
                var result = await staticFileService.Upload(file);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
