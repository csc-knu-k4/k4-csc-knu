using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace OsvitaApp.Models.ObservableModels;

public partial class MaterialObservableModel : ObservableObject
{
    
    public int Id { get; set; }
    public string Title { get; set; }
    public int TopicId { get; set; }
    public int OrderPosition { get; set; }
    
    [ObservableProperty] private ObservableCollection<ContentBlockObservableModel> _contentBlocks;
    
}