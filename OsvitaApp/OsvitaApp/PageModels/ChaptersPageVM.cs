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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ChaptersPageDto _chaptersPageDto;
        private ChapterObservableModel _selectedChapter;


        [ObservableProperty] private ObservableCollection<ChapterObservableModel> _chapters;

        public ChaptersPageVM(ISubjectsService subjectsService, IChaptersService chaptersService, IUserService userService, IMapper mapper, ChaptersPageDto chaptersPageDto)
        {
            _subjectsService = subjectsService;
            _chaptersService = chaptersService;
            _userService = userService;
            _mapper = mapper;
            _chaptersPageDto = chaptersPageDto;
        }


        [RelayCommand]
        private async Task NavigatedTo()
        {
            var res = await _subjectsService.GetChaptersAsync(_chaptersPageDto.Subject.Id);
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
        private async Task ChapterExpanded(ChapterObservableModel chapter)
        {
            chapter.IsExpanded = !chapter.IsExpanded;
            if(chapter.IsExpanded)
            {
                _selectedChapter = chapter;
            }
            else
            {
                _selectedChapter = null;
            }
            if(chapter.IsExpanded)
            {
                foreach(var item in Chapters.Where(x => x != chapter))
                {
                    item.IsExpanded = false;
                }
            }

            if(!chapter.Topics?.Any() ?? true && chapter.IsExpanded)
            {
                var res = await _chaptersService.GetTopicsAsync(chapter.Id);
                if(res.IsSuccess)
                {
                    chapter.Topics = res.Data.Select(_mapper.Map<TopicObservableModel>).ToObservableCollection();
                    var userStatistics = _userService.GetStatistic();
                    foreach(var topic in chapter.Topics)
                    {
                        if(userStatistics.TopicProgressDetails.Any(x => x.TopicId == topic.Id))
                        {
                            topic.IsChecked = true;
                        }
                    }
                }
                else
                {
                    await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
                }
            }

            if(chapter.Topics.Any(x => !x.IsChecked))
            {
                chapter.Topics.FirstOrDefault(x => !x.IsChecked).IsSelected = true;
            }
        }

        [RelayCommand]
        private async Task TopicClicked(TopicObservableModel topic)
        {
            topic.IsSelected = !topic.IsSelected;
            if(topic.IsSelected)
            {
                foreach(var item in _selectedChapter.Topics.Where(x => x != topic))
                {
                    item.IsSelected = false;
                }
            }
        }

        [RelayCommand]
        public async Task TopicsMaterialButtonClicked(TopicObservableModel topic)
        {
            
        }

        [RelayCommand]
        public async Task TopicsTestButtonClicked(TopicObservableModel topic)
        {

        }
    }
}
