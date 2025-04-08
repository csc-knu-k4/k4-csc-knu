using System.Globalization;

namespace OsvitaApp.Helpers.Converters;

public class ImageFromUriConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string uri && !string.IsNullOrWhiteSpace(uri))
        {
            try
            {
                return ImageSource.FromUri(new Uri(new Uri(ApiService.BaseUrl), uri));
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