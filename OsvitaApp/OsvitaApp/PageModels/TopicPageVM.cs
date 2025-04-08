using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OsvitaApp.Interfaces;
using OsvitaApp.Models.Dto;
using OsvitaApp.Models.ObservableModels;

namespace OsvitaApp.PageModels;

public partial class TopicPageVM : ObservableObject
{
    private readonly ITopicsService _topicsService;
    private readonly TopicPageDto _topicPageDto;
    private readonly IMapper _mapper;
    private readonly MaterialPageDto _materialPageDto;
    private readonly TestPageDto _testPageDto;
    
    [ObservableProperty] private TopicObservableModel _topic;
    [ObservableProperty] private ObservableCollection<MaterialObservableModel> _materials;


    public TopicPageVM(ITopicsService topicService, IMapper mapper, TopicPageDto topicPageDto, MaterialPageDto materialPageDto, TestPageDto testPageDto)
    {
        _topicsService = topicService;
        _mapper = mapper;
        _topicPageDto = topicPageDto;
        _materialPageDto = materialPageDto;
        Topic = _topicPageDto.Topic;
        _testPageDto = testPageDto;
        
    }
    
    [RelayCommand]
    private async Task NavigatedTo()
    {
        var res = await _topicsService.GetMaterialsAsync(_topicPageDto.Topic.Id);
        if (res.IsSuccess)
        {
            Materials = res.Data.Select(_mapper.Map<MaterialObservableModel>).ToObservableCollection();
        }
        else
        {
            await AppShell.DisplaySnackbarAsync(res.ErrorMessage);
        }
    }

    [RelayCommand]
    private async Task MaterialClicked(MaterialObservableModel material)
    {
        _materialPageDto.Material = material;
        await Shell.Current.GoToAsync($"material");
    }
    
    [RelayCommand]
    private async Task TakeTestClicked()
    {
        _testPageDto.Topic = _topicPageDto.Topic;
        _testPageDto.TestType = TestType.TopicTest;
        await Shell.Current.GoToAsync($"test");
    }
}