using OsvitaBLL.Interfaces;
using Quartz;

namespace OsvitaWebApiPL.Jobs
{
    public class AddDailyAssignmentJob : IJob
    {
        private readonly IUserService userService;
        private readonly IDailyAssignmentService dailyAssignmentService;
        public AddDailyAssignmentJob(IUserService userService, IDailyAssignmentService dailyAssignmentService)
        {
            this.userService = userService;
            this.dailyAssignmentService = dailyAssignmentService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var users = await userService.GetAllAsync();

            foreach (var user in users)
            {
                int count = await dailyAssignmentService.CountDailySetsToAdd(user.Id);
                for (int i = 0; i < count; i++)
                {
                    await dailyAssignmentService.AddDailyAssignmentAsync(user.Id);
                }
            }
        }
    }
}

