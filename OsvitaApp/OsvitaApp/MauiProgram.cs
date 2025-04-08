using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using OsvitaApp.Helpers.Handlers;
using OsvitaApp.Interfaces;
using OsvitaApp.Mappers;
using OsvitaApp.Models.Dto;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace OsvitaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options => options.SetShouldEnableSnackbarOnWindows(true))
                .ConfigureSyncfusionToolkit()
                .UseSkiaSharp()
                .UseAnswerOptionsControl()
                .ConfigureMauiHandlers(handlers =>
                {
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                    fonts.AddFont("Inter-VariableFont.ttf", "Inter");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                });

#if DEBUG
    		builder.Logging.AddDebug();
    		builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            builder.Services.AddSingleton<ModalErrorHandler>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<ISubjectsService, SubjectsService>();
            builder.Services.AddSingleton<IChaptersService, ChaptersService>();
            builder.Services.AddSingleton<ITopicsService, TopicsService>();
            builder.Services.AddSingleton<IMaterialService, MaterialService>();
            builder.Services.AddSingleton<IAssignmentsService, AssignmentsService>();
            
            builder.Services.AddTransient<LoginPage, LoginPageVM>();
            builder.Services.AddTransientWithShellRoute<RegistrationPage, RegistrationPageVM>("registration");
            builder.Services.AddTransientWithShellRoute<SubjectsPage, SubjectsPageVM>("subjects");
            builder.Services.AddTransientWithShellRoute<SubjectPage, SubjectPageVM>("subject");
            builder.Services.AddTransientWithShellRoute<ChapterPage, ChapterPageVM>("chapter");
            builder.Services.AddTransientWithShellRoute<TopicPage, TopicPageVM>("topic");
            builder.Services.AddTransientWithShellRoute<MaterialPage, MaterialPageVM>("material");
            builder.Services.AddTransientWithShellRoute<TestPage, TestPageVM>("test");

            builder.Services.AddSingleton<SubjectPageDto>();
            builder.Services.AddSingleton<MaterialPageDto>();
            builder.Services.AddSingleton<ChapterPageDto>();
            builder.Services.AddSingleton<TopicPageDto>();
            builder.Services.AddSingleton<TestPageDto>();
            return builder.Build();
        }
    }
}
