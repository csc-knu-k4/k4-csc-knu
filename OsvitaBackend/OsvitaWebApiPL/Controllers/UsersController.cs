using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IStatisticService statisticService;
        public UsersController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
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
        public async Task<ActionResult> PostAssignmentSetProgressDetail(int id, [FromBody] List<AssignmentSetProgressDetailModel> models)
        {
            try
            {
                await statisticService.AddAssignmentSetProgressDetailsAsync(models, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/users/5/statistic/assignments
        [HttpPut("{id}/statistic/assignments")]
        public async Task<ActionResult> PutAssignmentSetProgressDetail(int id, [FromBody] List<AssignmentSetProgressDetailModel> models)
        {
            try
            {
                await statisticService.UpdateAssignmentSetProgressDetailsAsync(models, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
