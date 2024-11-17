using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Identity
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = RoleSettings.StudentRole,
                    NormalizedName = RoleSettings.NormalizedStudentRole
                },
                new IdentityRole
                {
                    Name = RoleSettings.AdminRole,
                    NormalizedName = RoleSettings.NormalizedAdminRole
                },
                new IdentityRole
                {
                    Name = RoleSettings.TeacherRole,
                    NormalizedName = RoleSettings.NormalizedTeacherRole
                }
            );
        }

        public static async Task InitializeAsync(UserManager<OsvitaUser> userManager, IUserService userService, IConfiguration configuration)
        {
            string adminEmail = configuration.GetSection(SettingStrings.AdminSettings).GetSection(RoleSettings.Email).Value;
            string password = configuration.GetSection(SettingStrings.AdminSettings).GetSection(RoleSettings.Password).Value;

            if (await userManager.FindByNameAsync(adminEmail) is null)
            {
                OsvitaUser admin = new OsvitaUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                    await userManager.ConfirmEmailAsync(admin, code);
                    await userManager.AddToRoleAsync(admin, RoleSettings.AdminRole);
                }
            }
            if (await userService.GetByEmailAsync(adminEmail) is null)
            {
                var userModel = new UserModel { Email = adminEmail };
                await userService.AddAsync(userModel);
            }
        }
    }
}

