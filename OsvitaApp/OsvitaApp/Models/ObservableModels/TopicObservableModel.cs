using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.ObservableModels
{
    public partial class TopicObservableModel : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ChapterId { get; set; }
        public int OrderPosition { get; set; }

        [ObservableProperty] public bool _isSelected;
        [ObservableProperty] public bool _isChecked;
    }
}
