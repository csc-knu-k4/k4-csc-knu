using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/assistant")]
    [ApiController]
    public class AssistantController : ControllerBase
    {
        private readonly IAIService aiService;

        public AssistantController(IAIService aiService)
        {
            this.aiService = aiService;
        }

        // POST api/assistant/definition
        [HttpPost("definition")]
        public async Task<ActionResult<AssistantResponseModel>> GetDefinition([FromBody] AssistantRequestModel request)
        {
            try
            {
                var response = await aiService.GetChatAssistantResponseAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
