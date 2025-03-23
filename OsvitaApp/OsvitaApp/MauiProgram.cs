using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using OsvitaApp.Interfaces;
using OsvitaApp.Mappers;
using OsvitaApp.Models.Dto;
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

            builder.Services.AddSingleton<ProjectRepository>();
            builder.Services.AddSingleton<TaskRepository>();
            builder.Services.AddSingleton<CategoryRepository>();
            builder.Services.AddSingleton<TagRepository>();
            builder.Services.AddSingleton<SeedDataService>();
            builder.Services.AddSingleton<ModalErrorHandler>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<ISubjectsService, SubjectsService>();
            builder.Services.AddSingleton<IChaptersService, ChaptersService>();
            
            builder.Services.AddTransient<LoginPage, LoginPageVM>();
            builder.Services.AddTransientWithShellRoute<RegistrationPage, RegistrationPageVM>("registration");
            builder.Services.AddTransientWithShellRoute<SubjectsPage, SubjectsPageVM>("subjects");
            builder.Services.AddTransientWithShellRoute<ChaptersPage, ChaptersPageVM>("chapters");
            builder.Services.AddTransientWithShellRoute<MaterialPage, MaterialPageVM>("material");
            builder.Services.AddTransientWithShellRoute<TestPage, TestPageVM>("test");

            builder.Services.AddSingleton<ChaptersPageDto>();
            return builder.Build();
        }
    }
}
