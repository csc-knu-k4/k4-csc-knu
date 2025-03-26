using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels
{
    public partial class MaterialPageVM : ObservableObject
    {
        private readonly MaterialService _materialService;
        private readonly MaterialPageDto _dto;
        public MaterialPageVM(MaterialService materialService, MaterialPageDto dto)
        {
            _materialService = materialService;
            _dto = dto;
        }
        
        [RelayCommand]
        private async Task NavigatedTo()
        {
           
        }
    }
}
