using Microsoft.AspNetCore.Mvc;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(ILogger<FilesController> logger, IWebHostEnvironment environment) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("Invalid file");
                }
                var fileName = $"{Guid.NewGuid().ToString()}-{Path.GetExtension(file.FileName)}";
                var webrootPath = environment.WebRootPath;
                var path = Path.Combine(webrootPath, "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var fileUrl = $"{baseUrl}/uploads/{fileName}";
                return fileUrl;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
