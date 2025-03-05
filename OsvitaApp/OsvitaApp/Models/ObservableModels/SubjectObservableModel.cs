using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.Models.ObservableModels
{
    public partial class SubjectObservableModel : ObservableObject
    {
        [ObservableProperty] public int _id;
        [ObservableProperty] public string _title;
        [ObservableProperty] public List<int> _chaptersIds;
        [ObservableProperty] public bool _isEnabled;
    }
}
