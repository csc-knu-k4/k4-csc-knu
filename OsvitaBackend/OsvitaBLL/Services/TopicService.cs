using System;
using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class TopicService : ITopicService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly ITopicRepository topicRepository;
        private readonly IMapper mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            topicRepository = unitOfWork.TopicRepository;
            this.mapper = mapper;
        }

        public async Task AddAsync(TopicModel model)
        {
            var topic = mapper.Map<Topic>(model);
            await topicRepository.AddAsync(topic);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(TopicModel model)
        {
            await topicRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await topicRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TopicModel>> GetAllAsync()
        {
            var topics = await topicRepository.GetAllAsync();
            var topicsModels = mapper.Map<IEnumerable<Topic>, IEnumerable<TopicModel>>(topics);
            return topicsModels;
        }

        public async Task<IEnumerable<TopicModel>> GetByChapterIdAsync(int chapterId)
        {
            var topics = await topicRepository.GetAllAsync();
            topics = topics?.Where(x => x.ChapterId == chapterId);
            var topicsModels = topics is not null ? mapper.Map<IEnumerable<Topic>, IEnumerable<TopicModel>>(topics) : new List<TopicModel>();
            return topicsModels;
        }

        public async Task<IEnumerable<TopicModel>> GetByFilterAsync(FilterSearchModel filterSearchModel)
        {
            var topics = await topicRepository.GetAllAsync();
            topics = topics?.Where(x =>
                    (filterSearchModel.SubjectId is null || x.Chapter.SubjectId == filterSearchModel.SubjectId) &&
                    (filterSearchModel.ChapterId is null || x.ChapterId == filterSearchModel.ChapterId) &&
                    (String.IsNullOrEmpty(filterSearchModel.SearchString) || x.Title.Contains(filterSearchModel.SearchString))
                );
            var topicsModels = topics is not null ? mapper.Map<IEnumerable<Topic>, IEnumerable<TopicModel>>(topics) : new List<TopicModel>();
            return topicsModels;
        }

        public async Task<TopicModel> GetByIdAsync(int id)
        {
            var topic = await topicRepository.GetByIdAsync(id);
            var topicModel = mapper.Map<Topic, TopicModel>(topic);
            return topicModel;
        }

        public async Task UpdateAsync(TopicModel model)
        {
            var topic = mapper.Map<Topic>(model);
            await topicRepository.UpdateAsync(topic);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

