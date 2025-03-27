using System.Globalization;

namespace OsvitaApp.Helpers.Converters;

public class Base64ToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string base64String && !string.IsNullOrWhiteSpace(base64String))
        {
            try
            {
                byte[] imageBytes = System.Convert.FromBase64String(base64String);
                MemoryStream stream = new MemoryStream(imageBytes);

                // Оновлено: Оновлення позиції потоку
                stream.Position = 0;

                return ImageSource.FromStream(() => new MemoryStream(imageBytes)); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error decoding Base64 image: {ex.Message}");
            }
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}