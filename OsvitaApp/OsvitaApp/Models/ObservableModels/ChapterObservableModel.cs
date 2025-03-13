using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.ObservableModels
{
    public partial class ChapterObservableModel : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SubjectId { get; set; }
        public int OrderPosition { get; set; }
        [ObservableProperty] public bool _isExpanded;
        [ObservableProperty] public ObservableCollection<TopicObservableModel> _topics;

    }
}
