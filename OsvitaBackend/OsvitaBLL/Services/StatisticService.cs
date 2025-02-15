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
                statistic.ChapterProgressDetails = (await unitOfWork.StatisticRepository.GetChapterProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            }
            var statisticsModels = mapper.Map<IEnumerable<Statistic>, IEnumerable<StatisticModel>>(statistics);
            return statisticsModels;
        }

        public async Task<StatisticModel> GetByIdAsync(int id)
        {
            var statistic = await statisticRepository.GetByIdAsync(id);
            statistic.ChapterProgressDetails = (await unitOfWork.StatisticRepository.GetChapterProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            var statisticModel = mapper.Map<Statistic, StatisticModel>(statistic);
            return statisticModel;
        }

        public Task<StatisticModel> GetStatisticByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(StatisticModel model)
        {
            var statistic = mapper.Map<Statistic>(model);
            await statisticRepository.UpdateAsync(statistic);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

