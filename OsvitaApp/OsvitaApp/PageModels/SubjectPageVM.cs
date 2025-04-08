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
    public partial class SubjectPageVM : ObservableObject
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;
        private readonly SubjectPageDto _subjectPageDto;
        private readonly ChapterPageDto _chapterPageDto;
        [ObservableProperty] private SubjectObservableModel _subject;
        [ObservableProperty] private ObservableCollection<ChapterObservableModel> _chapters;

        public SubjectPageVM(ISubjectsService subjectsService, IMapper mapper, SubjectPageDto subjectPageDto, ChapterPageDto chapterPageDto)
        {
            _subjectsService = subjectsService;
            _mapper = mapper;
            _subjectPageDto = subjectPageDto;
            _chapterPageDto = chapterPageDto;
            Subject = _subjectPageDto.Subject;
        }


        [RelayCommand]
        private async Task NavigatedTo()
        {
            var res = await _subjectsService.GetChaptersAsync(Subject.Id);
            if(res.IsSuccess)
            {
                Chapters = res.Data.Select(_mapper.Map<ChapterObservableModel>).ToObservableCollection();
            }
            else
            {
                await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
            }
        }

        [RelayCommand]
        private async Task ChapterClicked(ChapterObservableModel chapter)
        {
            _chapterPageDto.Chapter = chapter;
            await Shell.Current.GoToAsync($"chapter");
        }
    }
}
