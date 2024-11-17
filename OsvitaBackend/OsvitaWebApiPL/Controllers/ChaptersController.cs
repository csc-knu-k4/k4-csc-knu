using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/chapters")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService chapterService;
        private readonly ITopicService topicService;
        public ChaptersController(IChapterService chapterService, ITopicService topicService)
        {
            this.chapterService = chapterService;
            this.topicService = topicService;
        }

        // GET: api/chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterModel>>> Get()
        {
            var chaptersModels = await chapterService.GetAllAsync();
            if (chaptersModels is not null)
            {
                return Ok(chaptersModels);
            }
            return NotFound();
        }

        // GET api/chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterModel>> Get(int id)
        {
            var chapterModel = await chapterService.GetByIdAsync(id);
            if (chapterModel is not null)
            {
                return Ok(chapterModel);
            }
            return NotFound();
        }

        // POST api/chapters
        [HttpPost]
        public async Task<ActionResult<ChapterModel>> Post([FromBody] ChapterModel model)
        {
            try
            {
                var id = await chapterService.AddAsync(model);
                var chapterModel = await chapterService.GetByIdAsync(id);
                return Ok(chapterModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/chapters/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ChapterModel model)
        {
            try
            {
                model.Id = id;
                await chapterService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/chapters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await chapterService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/chapters/5/topics
        [HttpGet("{id}/topics")]
        public async Task<ActionResult<IEnumerable<TopicModel>>> GetTopics(int id)
        {
            var topicModels = await topicService.GetByChapterIdAsync(id);
            if (topicModels is not null)
            {
                return Ok(topicModels);
            }
            return NotFound();
        }
    }
}
