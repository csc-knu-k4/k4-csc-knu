using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaBLL.Services
{
	public class StatisticService : IStatisticService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IStatisticRepository statisticRepository;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IMapper mapper;

        public StatisticService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            this.unitOfWork = unitOfWork;
            statisticRepository = unitOfWork.StatisticRepository;
            assignmentRepository = unitOfWork.AssignmentRepository;
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
            foreach (var assignmentProgressDetail in assignmentSetProgressDetail.AssignmentProgressDetails)
            {
                assignmentProgressDetail.IsCorrect = false;
                assignmentProgressDetail.Points = 0;
                if (model.IsCompleted)
                {
                    var assignment = await assignmentRepository.GetByIdWithDetailsAsync(assignmentProgressDetail.AssignmentId);
                    var answer = assignment.Answers.FirstOrDefault(x => x.AssignmentId == assignmentProgressDetail.AssignmentId && x.Value == assignmentProgressDetail.AnswerValue);
                    assignmentProgressDetail.IsCorrect = (answer is not null) && answer.IsCorrect;
                    assignmentProgressDetail.Points = (answer is not null) ? answer.Points ?? 0 : 0;
                }
            }
            assignmentSetProgressDetail.Score = 0;
            if (model.IsCompleted)
            {
                await FillEmptyAssignmentProgressDetailAsync(assignmentSetProgressDetail);
                assignmentSetProgressDetail.Score = assignmentSetProgressDetail.AssignmentProgressDetails.Where(x => x.IsCorrect).Sum(x => x.Points);
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
            statistic.AssignmentSetProgressDetails = (await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            var statisticModel = mapper.Map<Statistic, StatisticModel>(statistic);
            return statisticModel;
        }

        public async Task<StatisticModel> GetStatisticByUserIdAsync(int userId)
        {
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            statistic.TopicProgressDetails = (await unitOfWork.StatisticRepository.GetTopicProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            statistic.AssignmentSetProgressDetails = (await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id)).ToList();
            var statisticModel = mapper.Map<Statistic, StatisticModel>(statistic);
            foreach (var assignmentSetProgressDetail in statisticModel.AssignmentSetProgressDetails)
            {
                assignmentSetProgressDetail.MaxScore = 0;
                foreach (var assignmentProgressDetail in assignmentSetProgressDetail.AssignmentProgressDetails)
                {
                    var assignment = await assignmentRepository.GetByIdWithDetailsAsync(assignmentProgressDetail.AssignmentId);
                    var maxPoints = GetMaxPointsByAssignmentType(assignment.AssignmentType);
                    assignmentProgressDetail.MaxPoints = maxPoints;
                    assignmentSetProgressDetail.MaxScore += maxPoints;
                }
            }
            return statisticModel;
        }

        public async Task<AssignmentSetProgressDetailModel> GetAssignmentSetProgressDetailAsync(int userId, int assignmentSetProgressDetailId)
        {
            var statistic = await statisticRepository.GetStatisticByUserIdWithDetailsAsync(userId);
            var assignmentSetProgressDetail = (await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id)).FirstOrDefault(x => x.Id == assignmentSetProgressDetailId);
            var assignmentSetProgressDetailModel = mapper.Map<AssignmentSetProgressDetail, AssignmentSetProgressDetailModel>(assignmentSetProgressDetail);
            assignmentSetProgressDetailModel.MaxScore = 0;
            foreach (var assignmentProgressDetail in assignmentSetProgressDetailModel.AssignmentProgressDetails)
            {
                var assignment = await assignmentRepository.GetByIdWithDetailsAsync(assignmentProgressDetail.AssignmentId);
                var maxPoints = GetMaxPointsByAssignmentType(assignment.AssignmentType);
                assignmentProgressDetail.MaxPoints = maxPoints;
                assignmentSetProgressDetailModel.MaxScore += maxPoints;
            }
            return assignmentSetProgressDetailModel;
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
            var oldAssignmentSetProgressDetail = statistic.AssignmentSetProgressDetails.FirstOrDefault(x => x.Id == model.Id);
            if (!oldAssignmentSetProgressDetail.IsCompleted)
            {
                foreach (var assignmentProgressDetail in model.AssignmentProgressDetails)
                {
                    var oldAssignmentProgressDetail = oldAssignmentSetProgressDetail.AssignmentProgressDetails.FirstOrDefault(x => x.AssignmentId == assignmentProgressDetail.AssignmentId && x.AssignmentSetProgressDetailId == assignmentProgressDetail.AssignmentSetProgressDetailId);
                    if (oldAssignmentProgressDetail is not null)
                    {
                        oldAssignmentProgressDetail.AnswerValue = assignmentProgressDetail.AnswerValue;
                        oldAssignmentProgressDetail.IsCorrect = false;
                        oldAssignmentProgressDetail.Points = 0;

                    }
                    else
                    {
                        oldAssignmentProgressDetail = mapper.Map<AssignmentProgressDetail>(assignmentProgressDetail);
                        oldAssignmentProgressDetail.IsCorrect = false;
                        oldAssignmentProgressDetail.Points = 0;
                        oldAssignmentSetProgressDetail.AssignmentProgressDetails.Add(oldAssignmentProgressDetail);
                    }
                }
                if (model.IsCompleted)
                {
                    foreach (var assignmentProgressDetail in oldAssignmentSetProgressDetail.AssignmentProgressDetails)
                    {
                        var assignment = await assignmentRepository.GetByIdWithDetailsAsync(assignmentProgressDetail.AssignmentId);
                        var answer = assignment.Answers.FirstOrDefault(x => x.AssignmentId == assignmentProgressDetail.AssignmentId && x.Value == assignmentProgressDetail.AnswerValue);
                        assignmentProgressDetail.IsCorrect = (answer is not null) && answer.IsCorrect;
                        assignmentProgressDetail.Points = (answer is not null) ? answer.Points ?? 0 : 0;
                    }
                }
                oldAssignmentSetProgressDetail.IsCompleted = model.IsCompleted;
                oldAssignmentSetProgressDetail.CompletedDate = model.CompletedDate;
                if (oldAssignmentSetProgressDetail.IsCompleted)
                {
                    await FillEmptyAssignmentProgressDetailAsync(oldAssignmentSetProgressDetail);
                    oldAssignmentSetProgressDetail.Score = oldAssignmentSetProgressDetail.AssignmentProgressDetails.Where(x => x.IsCorrect).Sum(x => x.Points);
                }
            }
            await unitOfWork.SaveChangesAsync();
            return oldAssignmentSetProgressDetail.Id;
        }

        private async Task FillEmptyAssignmentProgressDetailAsync(AssignmentSetProgressDetail assignmentSetProgressDetail)
        {
            var assignmentsIds = (await unitOfWork.AssignmentLinkRepository.GetAllAsync()).Where(x => x.ObjectId == assignmentSetProgressDetail.AssignmentSetId && x.ObjectType == ObjectType.AssignmentSet).Select(x => x.AssignmentId).ToList();
            var assignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment && assignmentsIds.Contains(x.Id)).ToList();
            foreach (var assignment in assignments)
            {
                if (assignment.AssignmentType == AssignmentType.MatchComplianceAssignment)
                {
                    var childAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).Where(x => x.ParentAssignmentId == assignment.Id);
                    foreach (var childAssignment in childAssignments)
                    {
                        if (!assignmentSetProgressDetail.AssignmentProgressDetails.Select(x => x.AssignmentId).Contains(childAssignment.Id))
                        {
                            var assignmentProgressDetail = new AssignmentProgressDetail
                            {
                                AssignmentSetProgressDetailId = assignmentSetProgressDetail.Id,
                                AssignmentId = childAssignment.Id,
                                AnswerValue = string.Empty,
                                IsCorrect = false,
                                Points = 0
                            };
                            assignmentSetProgressDetail.AssignmentProgressDetails.Add(assignmentProgressDetail);
                        }
                    }
                }
                else
                {
                    if (!assignmentSetProgressDetail.AssignmentProgressDetails.Select(x => x.AssignmentId).Contains(assignment.Id))
                    {
                        var assignmentProgressDetail = new AssignmentProgressDetail
                        {
                            AssignmentSetProgressDetailId = assignmentSetProgressDetail.Id,
                            AssignmentId = assignment.Id,
                            AnswerValue = string.Empty,
                            IsCorrect = false,
                            Points = 0
                        };
                        assignmentSetProgressDetail.AssignmentProgressDetails.Add(assignmentProgressDetail);
                    }
                }
            }
        }

        private int GetMaxPointsByAssignmentType(AssignmentType assignmentType)
        {
            var result = 0;
            switch (assignmentType)
            {
                case AssignmentType.OneAnswerAsssignment:
                    result = 1;
                    break;
                case AssignmentType.OpenAnswerAssignment:
                    result = 2;
                    break;
                case AssignmentType.ChildAssignment:
                    result = 1;
                    break;
                case AssignmentType.MatchComplianceAssignment:
                    result = 3;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

