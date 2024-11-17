using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/contentblocks")]
    [ApiController]
    public class ContentBlocksController : ControllerBase
    {
        private readonly IContentBlockService contentBlockService;
        public ContentBlocksController(IContentBlockService contentBlockService)
        {
            this.contentBlockService = contentBlockService;
        }

        // GET: api/contentblocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentBlockModel>>> Get()
        {
            var contentBlocksModels = await contentBlockService.GetAllAsync();
            if (contentBlocksModels is not null)
            {
                return Ok(contentBlocksModels);
            }
            return NotFound();
        }

        // GET api/contentblocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentBlockModel>> Get(int id)
        {
            var contentBlockModel = await contentBlockService.GetByIdAsync(id);
            if (contentBlockModel is not null)
            {
                return Ok(contentBlockModel);
            }
            return NotFound();
        }

        // POST api/contentblocks
        [HttpPost]
        public async Task<ActionResult<ContentBlockModel>> Post([FromBody] ContentBlockModel model)
        {
            try
            {
                var id = await contentBlockService.AddAsync(model);
                var contentBlockModel = await contentBlockService.GetByIdAsync(id);
                return Ok(contentBlockModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/contentblocks/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ContentBlockModel model)
        {
            try
            {
                model.Id = id;
                await contentBlockService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/contentblocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await contentBlockService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
