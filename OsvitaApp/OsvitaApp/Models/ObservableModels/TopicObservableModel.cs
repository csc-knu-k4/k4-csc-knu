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
        public string Title { get; set; }
        public int ChapterId { get; set; }
        public int OrderPosition { get; set; }
    }
}
