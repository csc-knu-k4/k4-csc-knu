using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class EducationClassesController : ControllerBase
    {
        private readonly IEducationClassService educationClassService;
        public EducationClassesController(IEducationClassService educationClassService)
        {
            this.educationClassService = educationClassService;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationClassModel>>> Get()
        {
            var educationClassesModels = await educationClassService.GetAllAsync();
            if (educationClassesModels is not null)
            {
                return Ok(educationClassesModels);
            }
            return NotFound();
        }

        // GET api/classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationClassModel>> Get(int id)
        {
            var educationClassesModel = await educationClassService.GetByIdAsync(id);
            if (educationClassesModel is not null)
            {
                return Ok(educationClassesModel);
            }
            return NotFound();
        }

        // POST api/classes
        [HttpPost]
        public async Task<ActionResult<ChapterModel>> Post([FromBody] EducationClassModel model)
        {
            try
            {
                var id = await educationClassService.AddAsync(model);
                var chapterModel = await educationClassService.GetByIdAsync(id);
                return Ok(chapterModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/classes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EducationClassModel model)
        {
            try
            {
                model.Id = id;
                await educationClassService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/classes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await educationClassService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/classes/5/students
        [HttpDelete("{id}/students/{studentId}")]
        public async Task<ActionResult> Delete(int id, int studentId)
        {
            try
            {
                await educationClassService.DeleteStudentByIdAsync(studentId, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
