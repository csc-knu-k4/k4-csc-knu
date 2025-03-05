using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using OsvitaApp.Models;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaApp.PageModels
{
    public partial class ChaptersPageVM : ObservableObject
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IChaptersService _chaptersService;
        private readonly IMapper _mapper;
        private readonly ChaptersPageDto _chaptersPageDto;


        [ObservableProperty] private ObservableCollection<ChapterObservableModel> _chapters;

        public ChaptersPageVM(ISubjectsService subjectsService, IChaptersService chaptersService, IMapper mapper, ChaptersPageDto chaptersPageDto)
        {
            _subjectsService = subjectsService;
            _chaptersService = chaptersService;
            _mapper = mapper;
            _chaptersPageDto = chaptersPageDto;
        }


        [RelayCommand]
        private async Task NavigatedTo()
        {
            var res = await _subjectsService.GetChaptersAsync(_chaptersPageDto.Subject.Id);
            if(res.IsSuccess)
            {
                
            }
            else
            {
                await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
            }
        }



    }
}
