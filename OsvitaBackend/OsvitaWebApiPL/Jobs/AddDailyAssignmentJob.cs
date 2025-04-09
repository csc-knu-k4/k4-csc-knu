using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Interfaces;
using Quartz;

namespace OsvitaWebApiPL.Jobs
{
    public class AddDailyAssignmentJob : IJob
    {
        private readonly IUserService userService;
        private readonly IIdentityService identityService;
        private readonly IAssignmentService assignmentService;
        public AddDailyAssignmentJob(IUserService userService, IIdentityService identityService)
        {
            this.userService = userService;
            this.identityService = identityService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var users = await userService.GetAllAsync();
            foreach (var user in users)
            {
                user.Roles = await identityService.GetUserRoles(user.Email);
            }
            var students = users.Where(x => x.Roles.Contains(RoleSettings.StudentRole));

            foreach (var student in students)
            {
                int countSetsToAdd =  await assignmentService.CountSetsToAdd(student.Id);
                for (int i = 0; i < countSetsToAdd; i++)
                {
                    await assignmentService.AddDailyAssignmentAsync(student.Id);
                }
            }
        }


    }
}

