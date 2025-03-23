using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels
{
    public partial class MaterialPageVM : ObservableObject
    {

        public MaterialPageVM()
        {

        }
        
        [RelayCommand]
        private async Task NavigatedTo()
        {
           
        }
    }
}
