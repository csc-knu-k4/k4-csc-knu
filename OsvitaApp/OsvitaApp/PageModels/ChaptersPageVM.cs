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
        private readonly ITopicsService _topicsService; 
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ChaptersPageDto _chaptersPageDto;
        private readonly MaterialPageDto _materialPageDto;
        private ChapterObservableModel _selectedChapter;


        [ObservableProperty] private ObservableCollection<ChapterObservableModel> _chapters;

        public ChaptersPageVM(ISubjectsService subjectsService, IChaptersService chaptersService, ITopicsService topicsService, IUserService userService, IMapper mapper, ChaptersPageDto chaptersPageDto, MaterialPageDto materialPageDto)
        {
            _subjectsService = subjectsService;
            _chaptersService = chaptersService;
            _topicsService = topicsService;
            _userService = userService;
            _mapper = mapper;
            _chaptersPageDto = chaptersPageDto;
            _materialPageDto = materialPageDto;
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
                foreach(var item in Chapters.Where(x => x != chapter && x.IsExpanded))
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
                TopicClicked(chapter.Topics.FirstOrDefault(x => !x.IsChecked));
                //chapter.Topics.FirstOrDefault(x => !x.IsChecked).IsSelected = true;
            }
        }

        [RelayCommand]
        private async Task TopicClicked(TopicObservableModel topic)
        {
            topic.IsSelected = !topic.IsSelected;
            if(topic.IsSelected)
            {
                if(!topic.Materials?.Any() ?? true)
                {
                    var getMaterialsResult = await _topicsService.GetMaterialsAsync(topic.Id);
                    if (getMaterialsResult.IsSuccess)
                    {
                        topic.Materials = getMaterialsResult.Data.Select(_mapper.Map<MaterialObservableModel>)
                            .ToObservableCollection();
                    }
                }
                foreach(var item in _selectedChapter.Topics.Where(x => x != topic && x.IsSelected))
                {
                    item.IsSelected = false;
                }
            }
        }

        [RelayCommand]
        public async Task MaterialClicked(MaterialObservableModel material)
        {
            _materialPageDto.Material = material;
            await Shell.Current.GoToAsync($"material");
        }

        [RelayCommand]
        public async Task TopicsTestButtonClicked(TopicObservableModel topic)
        {

        }
    }
}
