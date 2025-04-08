using OsvitaApp.Pages.Controls;
using SkiaSharp.Views.Maui.Handlers;

namespace OsvitaApp.Helpers.Handlers;

public static class Registration
{
    public static MauiAppBuilder UseAnswerOptionsControl(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(h =>
        {
            h.AddHandler<AnswerOptionsControl, SKCanvasViewHandler>();
        });

        return builder;
    }
}