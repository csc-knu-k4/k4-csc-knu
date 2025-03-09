using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaWebApiPL.Interfaces;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IStatisticService statisticService;
        private readonly IStatisticReportService statisticReportService;
        private readonly IUserService userService;
        private readonly IIdentityService identityService;
        private readonly IEducationPlanService educationPlanService;

        public UsersController(IStatisticService statisticService, IUserService userService, IIdentityService identityService, IStatisticReportService statisticReportService, IEducationPlanService educationPlanService)
        {
            this.statisticService = statisticService;
            this.userService = userService;
            this.identityService = identityService;
            this.statisticReportService = statisticReportService;
            this.educationPlanService = educationPlanService;
        }

        // GET api/users/5/statistic/
        [HttpGet("{id}/statistic")]
        public async Task<ActionResult<StatisticModel>> GetStatistic(int id)
        {
            var statisticModel = await statisticService.GetStatisticByUserIdAsync(id);
            if (statisticModel is not null)
            {
                return Ok(statisticModel);
            }
            return NotFound();
        }

        // POST api/users/5/statistic/topics
        [HttpPost("{id}/statistic/topics")]
        public async Task<ActionResult> PostTopicProgressDetail(int id, [FromBody] TopicProgressDetailModel model)
        {
            try
            {
                await statisticService.AddTopicProgressDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/users/5/statistic/topics
        [HttpPut("{id}/statistic/topics")]
        public async Task<ActionResult> PutTopicProgressDetail(int id, [FromBody] TopicProgressDetailModel model)
        {
            try
            {
                await statisticService.UpdateTopicProgressDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/users/5/statistic/assignments
        [HttpPost("{id}/statistic/assignments")]
        public async Task<ActionResult> PostAssignmentSetProgressDetail(int id, [FromBody] AssignmentSetProgressDetailModel model)
        {
            try
            {
                await statisticService.AddAssignmentSetProgressDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/users/5/statistic/assignments
        [HttpPut("{id}/statistic/assignments")]
        public async Task<ActionResult> PutAssignmentSetProgressDetail(int id, [FromBody] AssignmentSetProgressDetailModel model)
        {
            try
            {
                await statisticService.UpdateAssignmentSetProgressDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                var userModel = await userService.GetByIdAsync(id);
                userModel.Roles = await identityService.GetUserRoles(userModel.Email);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users/5/Educationplan/topics
        [HttpGet("{id}/Educationplan/topics")]
        public async Task<ActionResult<EducationPlanModel>> GetEducationPlan(int id)
        {
            var educationPlanModel = await educationPlanService.GetEducationPlanByUserIdAsync(id);
            if (educationPlanModel is not null)
            {
                return Ok(educationPlanModel);
            }
            return NotFound();
        }

        //GET api/users/5/Educationplan/topics/4
        [HttpGet("{id}/Educationplan/topics/{topicId}")]
        public async Task<ActionResult<TopicPlanDetailModel>> GetTopicPlanDetail(int id, int topicId)
        {
            var topicPlanDetailModel = await educationPlanService.GetTopicPlanDetailAsync(id, topicId);
            if (topicPlanDetailModel is not null)
            {
                return Ok(topicPlanDetailModel);
            }
            return NotFound();
        }

        // POST api/users/5/Educationplan/topics
        [HttpPost("{id}/Educationplan/topics")]
        public async Task<ActionResult> PostTopicPlanDetail(int id, [FromBody] TopicPlanDetailModel model)
        {
            try
            {
                await educationPlanService.AddTopicPlanDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/users/5/Educationplan/topics/3
        [HttpDelete("{id}/Educationplan/topics/{topicId}")]
        public async Task<ActionResult> DeleteTopicPlanDetail(int id, int topicId)
        {
            try
            {
                await educationPlanService.DeleteTopicPlanDetailAsync(id, topicId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/users/5/Educationplan/topics/3
        [HttpPut("{id}/Educationplan/topic/{topicId}")]
        public async Task<ActionResult> PutTopicPlanDetail(int id, int topicId, [FromBody] TopicPlanDetailModel model)
        {
            try
            {
                await educationPlanService.UpdateTopicPlanDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
