using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OsvitaBLL.Interfaces;
using OsvitaDAL.Entities;
using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Jobs;
using Quartz;
using Quartz.AspNetCore;

namespace OsvitaWebApiPL.Configurations
{
	public static class Extensions
	{
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<OsvitaUser>(options => options.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<OsvitaIdentityDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(SettingStrings.JwtSection);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection(SettingStrings.JwtIssuer).Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection(SettingStrings.JwtKey).Value)),
                    };
                });
        }

        public static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the Bearer scheme. Enter <\"Bearer\" [space] token>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OsvitaWebApi", Version = "v1" });
            });
        }

        public static async Task InitializeAdminAsync(this WebApplication webApp)
        {

            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<OsvitaUser>>();
                    var userService = services.GetRequiredService<IUserService>();
                    await RoleConfiguration.InitializeAsync(userManager, userService, webApp.Configuration);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database." + DateTime.Now.ToString());
                }
            }
        }

        public static void AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("AddRecomendationMessagesJob");
                q.AddJob<AddRecomendationMessagesJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("AddRecomendationMessagesJob-trigger")
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(10, 00))
                );
            });

            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("AddDailyAssignmentJob");
                q.AddJob<AddDailyAssignmentJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("AddDailyAssignmentJob-trigger-startup")
                    .StartNow()
                );

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("AddDailyAssignmentJob-trigger-weekly")
                    .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 0, 0))
                );
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}

