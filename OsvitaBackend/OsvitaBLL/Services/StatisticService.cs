using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class StatisticService : IStatisticService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IStatisticRepository statisticRepository;
        private readonly IMapper mapper;

        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            this.unitOfWork = unitOfWork;
            statisticRepository = unitOfWork.StatisticRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(StatisticModel model)
        {
            var statistic = mapper.Map<Statistic>(model);
            await statisticRepository.AddAsync(statistic);
            await unitOfWork.SaveChangesAsync();
            return statistic.Id;
        }

        public async Task<int> AddTopicProgressDetailAsync(TopicProgressDetailModel model, int userId)
        {
            var topicProgressDetail = mapper.Map<TopicProgressDetail>(model);
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            statistic.TopicProgressDetails.Add(topicProgressDetail);
            await statisticRepository.UpdateAsync(statistic);
            await unitOfWork.SaveChangesAsync();
            return topicProgressDetail.Id;
        }

        public async Task<int> AddAssignmentSetProgressDetailAsync(AssignmentSetProgressDetailModel model, int userId)
        {
            var assignmentSetProgressDetail = mapper.Map<AssignmentSetProgressDetail>(model);
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            if (statistic.AssignmentSetProgressDetails is null)
            {
                statistic.AssignmentSetProgressDetails = new List<AssignmentSetProgressDetail>();
            }
            statistic.AssignmentSetProgressDetails.Add(assignmentSetProgressDetail);
            await statisticRepository.UpdateAsync(statistic);
            await unitOfWork.SaveChangesAsync();
            return assignmentSetProgressDetail.Id;
        }

        public async Task DeleteAsync(StatisticModel model)
        {
            await statisticRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await statisticRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<StatisticModel>> GetAllAsync()
        {
            var statistics = await statisticRepository.GetAllAsync();
            foreach (var statistic in statistics)
            {
                statistic.TopicProgressDetails = (await unitOfWork.StatisticRepository.GetTopicProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            }
            var statisticsModels = mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticModel>>(statistics);
            return statisticsModels;
        }

        public async Task<StatisticModel> GetByIdAsync(int id)
        {
            var statistic = await statisticRepository.GetByIdAsync(id);
            statistic.TopicProgressDetails = (await unitOfWork.StatisticRepository.GetTopicProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            var statisticModel = mapper.Map<Statistic, StatisticModel>(statistic);
            return statisticModel;
        }

        public async Task<StatisticModel> GetStatisticByUserIdAsync(int userId)
        {
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            statistic.TopicProgressDetails = (await unitOfWork.StatisticRepository.GetTopicProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            statistic.AssignmentSetProgressDetails = (await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            var statisticModel = mapper.Map<Statistic, StatisticModel>(statistic);
            return statisticModel;
        }

        public async Task UpdateAsync(StatisticModel model)
        {
            var statistic = mapper.Map<Statistic>(model);
            await statisticRepository.UpdateAsync(statistic);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateTopicProgressDetailAsync(TopicProgressDetailModel model, int userId)
        {
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            var oldTopicProgressDetail = statistic.TopicProgressDetails.FirstOrDefault(x => x.TopicId == model.TopicId);
            oldTopicProgressDetail.IsCompleted = model.IsCompleted;
            oldTopicProgressDetail.CompletedDate = model.CompletedDate;
            await unitOfWork.SaveChangesAsync();
            return oldTopicProgressDetail.Id;
        }

        public async Task<int> UpdateAssignmentSetProgressDetailAsync(AssignmentSetProgressDetailModel model, int userId)
        {
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            var oldAssignmentSetProgressDetail = statistic.AssignmentSetProgressDetails.FirstOrDefault(x => x.StatisticId == model.StatisticId && x.AssignmentSetId == model.AssignmentSetId && x.AssignmentId == model.AssignmentId);
            oldAssignmentSetProgressDetail.AnswerId = model.AnswerId;
            await unitOfWork.SaveChangesAsync();
            return oldAssignmentSetProgressDetail.Id;
        }
    }
}

