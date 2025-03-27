using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels
{
    public partial class MaterialPageVM : ObservableObject
    {
        private readonly IMaterialService _materialService;
        private readonly MaterialPageDto _dto;
        private readonly IMapper _mapper;
        [ObservableProperty] private MaterialObservableModel _material;

        public MaterialPageVM(IMaterialService materialService, MaterialPageDto dto, IMapper mapper)
        {
            _materialService = materialService;
            _dto = dto;
            _material = _dto.Material;
            _mapper = mapper;
        }

        [RelayCommand]
        private async Task NavigatedTo()
        {
            try
            {
                if (!Material.ContentBlocks?.Any() ?? true)
                {
                    var getContentBlocksResult = await _materialService.GetContentBlocksAsync(Material.Id);
                    if (getContentBlocksResult.IsSuccess)
                    {
                        Material.ContentBlocks = getContentBlocksResult.Data
                            .Select(_mapper.Map<ContentBlockObservableModel>)
                            .OrderBy(x => x.OrderPosition)
                            .ThenBy(x => x.Id).ToObservableCollection();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}