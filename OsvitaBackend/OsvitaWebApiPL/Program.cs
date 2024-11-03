using Microsoft.EntityFrameworkCore;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Services;
using OsvitaDAL.Data;
using OsvitaDAL.Interfaces;
using OsvitaWebApiPL.Configurations;

namespace OsvitaWebApiPL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddDbContext<OsvitaDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString(SettingStrings.OsvitaDbConnection))
        );

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(OsvitaAutomapperProfile));
        builder.Services.AddTransient<ISubjectService, SubjectService>();
        builder.Services.AddTransient<IChapterService, ChapterService>();
        builder.Services.AddTransient<ITopicService, TopicService>();
        builder.Services.AddTransient<IMaterialService, MaterialService>();
        builder.Services.AddTransient<IContentBlockService, ContentBlockService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MigrateDatabase();

        app.MapControllers();

        app.Run();
    }
}

