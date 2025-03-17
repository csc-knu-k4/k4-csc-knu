using System;
using OsvitaBLL.Interfaces;
using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Interfaces;
using Quartz;

namespace OsvitaWebApiPL.Jobs
{
	public class AddRecomendationMessagesJob : IJob
    {
        private readonly IUserService userService;
        private readonly IIdentityService identityService;
        private readonly IRecomendationService recomendationService;
        public AddRecomendationMessagesJob(IUserService userService, IIdentityService identityService, IRecomendationService recomendationService)
        {
            this.userService = userService;
            this.identityService = identityService;
            this.recomendationService = recomendationService;
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
                await recomendationService.AddRecomendationMessageAsync(student.Id);
            }
        }
    }
}

