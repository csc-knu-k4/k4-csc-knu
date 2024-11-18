using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            this.assignmentService = assignmentService;
        }

        // GET: api/assignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentModel>>> Get()
        {
            var assignmentsModels = await assignmentService.GetAllAssignmentsAsync();
            if (assignmentsModels is not null)
            {
                return Ok(assignmentsModels);
            }
            return NotFound();
        }

        // GET api/assignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentModel>> Get(int id)
        {
            var assignmentModel = await assignmentService.GetAssignmentByIdAsync(id);
            if (assignmentModel is not null)
            {
                return Ok(assignmentModel);
            }
            return NotFound();
        }

        // POST api/assignments
        [HttpPost]
        public async Task<ActionResult<AssignmentModel>> Post([FromBody] AssignmentModel model)
        {
            try
            {
                var id = await assignmentService.AddAssignmentAsync(model);
                var assignmentModel = await assignmentService.GetAssignmentByIdAsync(id);
                return Ok(assignmentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/assignments/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AssignmentModel model)
        {
            try
            {
                model.Id = id;
                await assignmentService.UpdateAssignmentAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/assignments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await assignmentService.DeleteAssignmentByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
