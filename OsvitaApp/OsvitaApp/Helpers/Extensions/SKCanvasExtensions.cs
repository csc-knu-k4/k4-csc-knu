using SkiaSharp;

namespace OsvitaApp.Helpers.Extensions;

public static class SKCanvasExtensions
{
    public static void DrawTextCentered(this SKCanvas canvas, string text, SKRect rect, SKFont font, SKPaint paint)
    {
        if (string.IsNullOrEmpty(text) || canvas == null || font == null || paint == null)
            return;

        // Вимірюємо розмір тексту з SKFont
        var textBounds = new SKRect();
        font.MeasureText(text, out textBounds, paint);

        // Обчислюємо позицію для центрування
        float x = rect.MidX - textBounds.MidX;
        float y = rect.MidY - textBounds.MidY;

        // Малюємо текст
        canvas.DrawText(text, x, y, font, paint);
    }
}