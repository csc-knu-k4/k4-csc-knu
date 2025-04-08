using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels;

public partial class ChapterPageVM : ObservableObject
{
    private readonly IChaptersService _chaptersService;
    private readonly ChapterPageDto _chapterPageDto;
    private readonly IMapper _mapper;
    private readonly TopicPageDto _topicPageDto;

    [ObservableProperty] private ChapterObservableModel _chapter;
    [ObservableProperty] private ObservableCollection<TopicObservableModel> _topics;


    public ChapterPageVM(IChaptersService chaptersService, ChapterPageDto chapterPageDto, IMapper mapper, TopicPageDto topicPageDto)
    {
        _chaptersService = chaptersService;
        _chapterPageDto = chapterPageDto;
        _mapper = mapper;
        _topicPageDto = topicPageDto;
        Chapter = _chapterPageDto.Chapter;
    }

    [RelayCommand]
    private async Task NavigatedTo()
    {
        var res = await _chaptersService.GetTopicsAsync(_chapterPageDto.Chapter.Id);
        if (res.IsSuccess)
        {
            Topics = res.Data.Select(_mapper.Map<TopicObservableModel>).ToObservableCollection();
        }
        else
        {
            await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
        }
    }
    
    [RelayCommand]
    private async Task TopicClicked(TopicObservableModel topic)
    {
        _topicPageDto.Topic = topic;
        await Shell.Current.GoToAsync($"topic");
    }
}