using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaDAL.Entities;
using OsvitaWebApiPL.Interfaces;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IStatisticService statisticService;
        private readonly IUserService userService;
        private readonly IIdentityService identityService;
        private readonly IEducationPlanService educationPlanService;
        private readonly IRecomendationService recomendationService;
        private readonly IAssignmentService assignmentService;
        public UsersController(IStatisticService statisticService, IUserService userService, IIdentityService identityService, IEducationPlanService educationPlanService, IRecomendationService recomendationService, IAssignmentService assignmentService)
        {
            this.statisticService = statisticService;
            this.userService = userService;
            this.identityService = identityService;
            this.educationPlanService = educationPlanService;
            this.recomendationService = recomendationService;
            this.assignmentService = assignmentService;
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
                var assignmentSetProgressDetailId = await statisticService.AddAssignmentSetProgressDetailAsync(model, id);
                return Ok(assignmentSetProgressDetailId);
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
                var assignmentSetProgressDetailId = await statisticService.UpdateAssignmentSetProgressDetailAsync(model, id);
                return Ok(assignmentSetProgressDetailId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/statistic/assignments/{assignmentSetProgressDetailId}")]
        public async Task<ActionResult<AssignmentSetProgressDetailModel>> GetAssignmentSetProgressDetail(int id, int assignmentSetProgressDetailId)
        {
            try
            {
                var assignmentSetProgressDetail = await statisticService.GetAssignmentSetProgressDetailAsync(id, assignmentSetProgressDetailId);
                return Ok(assignmentSetProgressDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users/5/statistic/isdailyassignmentdone
        [HttpGet("{id}/statistic/isdailyassignmentdone")]
        public async Task<ActionResult<bool>> IsDailyAssignmentDone(int id)
        {
            try
            {
                var isDailyAssignmentDone = await statisticService.IsDailyAssignmentDoneAsync(id);
                return Ok(isDailyAssignmentDone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users/5/statistic/dailyassignment/streak
        [HttpGet("{id}/statistic/dailyassignment/streak")]
        public async Task<ActionResult<int>> GetDailyAssignmentStreak(int id, DateTime? fromDate)
        {
            try
            {
                var dailyAssignmentStreak = await statisticService.GetDailyAssignmentStreakAsync(id, fromDate);
                return Ok(dailyAssignmentStreak);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users/5/dailyassignment
        [HttpGet("{id}/dailyassignmentset")]
        public async Task<ActionResult<AssignmentSetProgressDetailModel>> GetDailyAssignmentSet(int id)
        {
            try
            {
                var dailyAssignmentSet = await assignmentService.GetDailyAssignmentSetAsync(id);
                return Ok(dailyAssignmentSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/users/5
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

        // GET api/users/5/educationplan
        [HttpGet("{id}/educationplan")]
        public async Task<ActionResult<EducationPlanModel>> GetEducationPlan(int id)
        {
            var educationPlanModel = await educationPlanService.GetEducationPlanByUserIdAsync(id);
            if (educationPlanModel is not null)
            {
                return Ok(educationPlanModel);
            }
            return NotFound();
        }

        //GET api/users/5/educationplan/topics/4
        [HttpGet("{id}/educationplan/topics/{topicId}")]
        public async Task<ActionResult<TopicPlanDetailModel>> GetTopicPlanDetail(int id, int topicId)
        {
            var topicPlanDetailModel = await educationPlanService.GetTopicPlanDetailAsync(id, topicId);
            if (topicPlanDetailModel is not null)
            {
                return Ok(topicPlanDetailModel);
            }
            return NotFound();
        }

        // POST api/users/5/educationplan/topics
        [HttpPost("{id}/educationplan/topics")]
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

        // DELETE api/users/5/educationplan/topics/3
        [HttpDelete("{id}/educationplan/topics/{topicId}")]
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

        // PUT: api/users/5/educationplan/topics/3
        [HttpPut("{id}/educationplan/topic/{topicId}")]
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

        // POST: api/users/5/educationplan/assignments
        [HttpPost("{id}/educationplan/assignments")]
        public async Task<ActionResult> PostAssignmentSetPlanDetail(int id, [FromBody] AssignmentSetPlanDetailModel model)
        {
            try
            {
                await educationPlanService.AddAssignmentSetPlanDetailAsync(model, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/users/5/educationplan/assignments/5
        [HttpDelete("{id}/educationplan/assignments/{assignmentSetId}")]
        public async Task<ActionResult> DeleteAssignmentSetProgressDetail(int id, int assignmentSetId)
        {
            try
            {
                await educationPlanService.DeleteAssignmentSetPlanDetailAsync(id, assignmentSetId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/users/5/recomendationmessages/
        [HttpGet("{id}/recomendationmessages/")]
        public async Task<ActionResult<IEnumerable<RecomendationMessageModel>>> GetRecomendationMessages(int id)
        {
            var messages = await recomendationService.GetRecomendationMessagesByUserIdAsync(id);
            if (messages is not null)
            {
                return Ok(messages);
            }
            return NotFound();
        }

        // PUT: api/users/5/recomendationmessages/3
        [HttpPut("{id}/recomendationmessages/{recomendationMessageId}")]
        public async Task<ActionResult> PutRecomendationMessage(int id, int recomendationMessageId, [FromBody] RecomendationMessageModel model)
        {
            try
            {
                model.Id = recomendationMessageId;
                model.UserId = id;
                await recomendationService.UpdateRecomendationMessageAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/users/5/recomendationmessages/3
        [HttpDelete("{id}/recomendationmessages/{recomendationMessageId}")]
        public async Task<ActionResult> DeleteRecomendationMessage(int id, int recomendationMessageId)
        {
            try
            {
                await recomendationService.DeleteRecomendationMessageByIdAsync(recomendationMessageId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/users/5/diagnosticalrecomendation/
        [HttpGet("{id}/diagnosticalrecomendation/")]
        public async Task<ActionResult<RecomendationAIModel>> GetDiagnosticalRecomendation(int id, int assignmentSetProgressDetailId)
        {
            var recomendation = await recomendationService.GetDiagnosticalRecomendationAsync(id, assignmentSetProgressDetailId);
            if (recomendation is not null)
            {
                return Ok(recomendation);
            }
            return NotFound();
        }
    }
}
