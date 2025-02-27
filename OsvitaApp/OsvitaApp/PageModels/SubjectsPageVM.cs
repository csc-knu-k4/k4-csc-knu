using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class SubjectsPageVM : ObservableObject
    {

        private readonly ISubjectsService _subjectsService;

        public SubjectsPageVM(ISubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }

        [RelayCommand]
        private async Task NavigatedTo()
        {
            var res = await _subjectsService.GetSubjectsAsync();
        }
    }
}
