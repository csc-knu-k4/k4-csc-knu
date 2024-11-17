using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;
        private readonly IChapterService chapterService;
        public SubjectsController(ISubjectService subjectService, IChapterService chapterService)
        {
            this.subjectService = subjectService;
            this.chapterService = chapterService;
        }

        // GET: api/subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectModel>>> Get()
        {
            var subjectsModels = await subjectService.GetAllAsync();
            if (subjectsModels is not null)
            {
                return Ok(subjectsModels);
            }
            return NotFound();
        }

        // GET api/subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectModel>> Get(int id)
        {
            var subjectModel = await subjectService.GetByIdAsync(id);
            if (subjectModel is not null)
            {
                return Ok(subjectModel);
            }
            return NotFound();
        }

        // POST api/subjects
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post([FromBody] SubjectModel model)
        {
            try
            {
                await subjectService.AddAsync(model);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/subjects/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Put(int id, [FromBody] SubjectModel model)
        {
            try
            {
                model.Id = id;
                await subjectService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/subjects/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await subjectService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/subjects/5/chapters
        [HttpGet("{id}/chapters")]
        public async Task<ActionResult<IEnumerable<ChapterModel>>> GetChapters(int id)
        {
            var chapterModels = await chapterService.GetBySubjectIdAsync(id);
            if (chapterModels is not null)
            {
                return Ok(chapterModels);
            }
            return NotFound();
        }
    }
}
