using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;
using OsvitaWebApiPL.Configurations;
using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Interfaces;
using OsvitaWebApiPL.Services;
using QuestPDF.Infrastructure;

namespace OsvitaWebApiPL;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost5173", policy =>
            {
                policy.AllowAnyOrigin() 
                    .AllowAnyHeader()                   
                    .AllowAnyMethod();                
            });
        });

        QuestPDF.Settings.License = LicenseType.Community;

        builder.Services.AddControllers();

        builder.Services.AddDbContext<OsvitaDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString(SettingStrings.OsvitaDbConnection));
                option.EnableSensitiveDataLogging();
            }
        );;
        
        builder.Services.AddDbContext<OsvitaIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(SettingStrings.IdentityOsvitaDbConnection))
        );

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.ConfigureIdentity();
        builder.Services.ConfigureJWT(builder.Configuration);

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(OsvitaAutomapperProfile));
        builder.Services.AddTransient<ISubjectService, SubjectService>();
        builder.Services.AddTransient<IChapterService, ChapterService>();
        builder.Services.AddTransient<ITopicService, TopicService>();
        builder.Services.AddTransient<IMaterialService, MaterialService>();
        builder.Services.AddTransient<IContentBlockService, ContentBlockService>();
        builder.Services.AddTransient<IAssignmentService, AssignmentService>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IEducationClassService, EducationClassService>();
        builder.Services.AddTransient<IStatisticService, StatisticService>();
        builder.Services.AddTransient<IEducationClassPlanService, EducationClassPlanService>();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<IStatisticReportService, StatisticReportService>();
        builder.Services.AddTransient<IEducationPlanService, EducationPlanService>();
        builder.Services.AddTransient<IAIService, OpenAIService>();
        builder.Services.AddTransient<IRecomendationService, RecomendationService>();
        builder.Services.AddTransient<IExcelService, ExcelService>();

        builder.Services.Configure<StaticFilesSettings>(builder.Configuration.GetSection(SettingStrings.StaticFilesSection));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(SettingStrings.JwtSection));
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(SettingStrings.MailSettings));
        builder.Services.Configure<HostSettings>(builder.Configuration.GetSection(SettingStrings.HostSection));
        builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection(SettingStrings.OpenAISettings));
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        if (builder.Configuration[SettingStrings.ImagesSetting] == "local")
        {
                    builder.Services.AddTransient<IStaticFileService, FilesystemStaticFileService>(
                    serviceProvider => new FilesystemStaticFileService(
                        serviceProvider.GetRequiredService<IOptions<StaticFilesSettings>>(),
                        serviceProvider.GetService<IWebHostEnvironment>().WebRootPath
                )
            );
        }

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerDoc();

        builder.Services.AddQuartzJobs();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors("AllowLocalhost5173");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MigrateDatabase();
        app.MigrateIdentityDatabase();

        await app.InitializeAdminAsync();

        app.MapControllers();

        app.Run();
    }
}

