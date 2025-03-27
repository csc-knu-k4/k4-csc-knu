using OsvitaApp.Enums;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.Helpers.TemplateSelectors;

public class ContentBlockTemplateSelector : DataTemplateSelector
{
    public DataTemplate TextContentBlockTemplate { get; set; }
    public DataTemplate ImageContentBlockTemplate { get; set; }

    
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var contentblock = item as ContentBlockObservableModel;
        if (contentblock.ContentBlockModelType == ContentBlockModelType.TextBlock)
        {
            return TextContentBlockTemplate;
        }
        else
        {
            return ImageContentBlockTemplate;
        }
    }
}