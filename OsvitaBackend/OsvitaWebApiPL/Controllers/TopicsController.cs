using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly IMaterialService materialService;
        public TopicsController(ITopicService topicService, IMaterialService materialService)
        {
            this.topicService = topicService;
            this.materialService = materialService;
        }

        // GET: api/topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicModel>>> Get()
        {
            var topicsModels = await topicService.GetAllAsync();
            if (topicsModels is not null)
            {
                return Ok(topicsModels);
            }
            return NotFound();
        }

        // GET api/topics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicModel>> Get(int id)
        {
            var topicModel = await topicService.GetByIdAsync(id);
            if (topicModel is not null)
            {
                return Ok(topicModel);
            }
            return NotFound();
        }

        // POST api/topics
        [HttpPost]
        public async Task<ActionResult<TopicModel>> Post([FromBody] TopicModel model)
        {
            try
            {
                var id = await topicService.AddAsync(model);
                var topicModel = await topicService.GetByIdAsync(id);
                return Ok(topicModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/topics/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TopicModel model)
        {
            try
            {
                model.Id = id;
                await topicService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/topics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await topicService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/topics/5/materials
        [HttpGet("{id}/materials")]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> GetMaterials(int id)
        {
            var materialModels = await materialService.GetByTopicIdAsync(id);
            if (materialModels is not null)
            {
                return Ok(materialModels);
            }
            return NotFound();
        }
    }
}
