using System;
using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaBLL.Services
{
	public class RecomendationService : IRecomendationService
	{
        private readonly IAIService aiService;
        private readonly IStatisticReportService statisticReportService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRecomendationMessageRepository recomendationMessageRepository;
        private readonly IMapper mapper;
        public RecomendationService(IAIService aiService, IStatisticReportService statisticReportService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.aiService = aiService;
            this.statisticReportService = statisticReportService;
            this.unitOfWork = unitOfWork;
            this.recomendationMessageRepository = unitOfWork.RecomendationMessageRepository;
            this.mapper = mapper;
        }

        public async Task AddRecomendationMessageAsync(int userId)
        {
            var assignmentSetsCount = 2;
            var assignmentSetReportModels = await statisticReportService.GetLastAssignmetSetsReportsAsync(userId, assignmentSetsCount);
            var assignmentReportModels = assignmentSetReportModels.SelectMany(x => x.Assignments).ToList();
            var recomendationMesageText = string.Empty;
            try
            {
                if (assignmentReportModels.Count > 0)
                {
                    recomendationMesageText = await aiService.GetRecomendationTextByAssignmentResultsAsync(assignmentReportModels);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (!string.IsNullOrEmpty(recomendationMesageText))
            {
                var recomendationMessage = new RecomendationMessage
                {
                    RecomendationText = recomendationMesageText,
                    CreationDate = DateTime.Now,
                    UserId = userId
                };
                try
                {
                    await recomendationMessageRepository.AddAsync(recomendationMessage);
                    await unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<RecomendationMessageModel>> GetRecomendationMessagesByUserIdAsync(int userId)
        {
            var recomendationMessages = (await recomendationMessageRepository.GetAllAsync()).Where(x => x.UserId == userId).ToList();
            var recomendationMessagesModels = mapper.Map<IEnumerable<RecomendationMessage>, IEnumerable<RecomendationMessageModel>>(recomendationMessages);
            return recomendationMessagesModels;
        }

        public async Task<RecomendationMessageModel> GetRecomendationMessageByIdAsync(int id)
        {
            var recomendationMessage = await recomendationMessageRepository.GetByIdAsync(id);
            var recomendationMessageModel = mapper.Map<RecomendationMessage, RecomendationMessageModel>(recomendationMessage);
            return recomendationMessageModel;
        }

        public async Task DeleteRecomendationMessageByIdAsync(int id)
        {
            await recomendationMessageRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRecomendationMessageAsync(RecomendationMessageModel model)
        {
            var recomendationMessage = mapper.Map<RecomendationMessage>(model);
            await recomendationMessageRepository.UpdateAsync(recomendationMessage);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<RecomendationAIModel> GetDiagnosticalRecomendationAsync(int userId, int assignmentSetId)
        {
            var assignmentSetReportModel = await statisticReportService.GetAssignmetSetReportModelAsync(userId, assignmentSetId);
            var recomendation = await aiService.GetRecomendationByDiagnosticalAssignmentSetResultAsync(assignmentSetReportModel);
            return recomendation;
        }
    }
}

