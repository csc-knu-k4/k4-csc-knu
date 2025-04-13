using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Entities;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class EducationClassesController : ControllerBase
    {
        private readonly IEducationClassService educationClassService;
        private readonly IEducationClassPlanService educationClassPlanService;
        private readonly IStatisticReportService statisticReportService;
        public EducationClassesController(IEducationClassService educationClassService, IEducationClassPlanService educationClassPlanService, IStatisticReportService statisticReportService)
        {
            this.educationClassService = educationClassService;
            this.educationClassPlanService = educationClassPlanService;
            this.statisticReportService = statisticReportService;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationClassModel>>> Get([FromQuery] int? teacherId)
        {
            IEnumerable<EducationClassModel> educationClassesModels;
            if (teacherId is not null)
            {
                educationClassesModels = await educationClassService.GetByTeacherIdAsync((int)teacherId);
            }
            else
            {
                educationClassesModels = await educationClassService.GetAllAsync();
            }
            
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

        // GET api/classes/5/educationplan
        [HttpGet("{id}/educationplan")]
        public async Task<ActionResult<EducationClassPlanModel>> GetEducationClassPlan(int id)
        {
            var educationClassPlanModel = await educationClassPlanService.GetEducationPlanByEducationClassIdAsync(id);
            if (educationClassPlanModel is not null)
            {
                return Ok(educationClassPlanModel);
            }
            return NotFound();
        }

        // POST api/classes
        [HttpPost]
        public async Task<ActionResult<EducationClassModel>> Post([FromBody] EducationClassModel model)
        {
            try
            {
                var id = await educationClassService.AddAsync(model);
                var educationClassModel = await educationClassService.GetByIdAsync(id);
                return Ok(educationClassModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/classes/5/educationplan/assignments
        [HttpPost("{id}/educationplan/assignments")]
        public async Task<ActionResult> PostAssignmentSetProgressDetail(int id, [FromBody] AssignmentSetPlanDetailModel model)
        {
            try
            {
                await educationClassPlanService.AddAssignmentSetPlanDetailAsync(model, id);
                return Ok();
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
        public async Task<ActionResult> DeleteStudent(int id, int studentId)
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

        // POST api/classes/5/students
        [HttpPost("{id}/students")]
        public async Task<ActionResult> PostStudent(int id, [FromBody] StudentDTO model)
        {
            try
            {
                await educationClassService.InviteStudentByEmailAsync(model.Email, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/classes/5/students/5/confirmation/guid
        [HttpGet("{id}/students/{userId}/confirmations/{guid}")]
        public async Task<ActionResult> ConfirmStudent(int id, int userId, string guid)
        {
            try
            {
                await educationClassService.ConfirmStudentAsync(userId, id, guid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/classes/5/educationplan/topics
        [HttpPost("{id}/educationplan/topics")]
        public async Task<ActionResult> PostTopicPlanDetail(int id, [FromBody] TopicPlanDetailModel model)
        {
            try
            {
                await educationClassPlanService.AddTopicPlanDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/classes/5/educationplan/topics/5
        [HttpDelete("{id}/educationplan/topics/{topicId}")]
        public async Task<ActionResult> DeleteTopicPlanDetail(int id, int topicId)
        {
            try
            {
                await educationClassPlanService.DeleteTopicPlanDetailAsync(id, topicId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/classes/5/statistic/assignments/5
        [HttpGet("{id}/statistic/assignments/{assignmentSetId}")]
        public async Task<ActionResult<EducationClassPlanModel>> GetEducationClassPlan(int id, int assignmentSetId)
        {
            var report = await statisticReportService.GetEducationClassAssignmetSetReportModelAsync(id, assignmentSetId);
            if (report is not null)
            {
                return Ok(report);
            }
            return NotFound();
        }
    }
}
