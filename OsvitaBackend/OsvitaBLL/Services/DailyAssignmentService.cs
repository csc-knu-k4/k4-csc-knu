using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Data;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaBLL.Services
{
    public class DailyAssignmentService : IDailyAssignmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IAssignmentSetRepository assignmentSetRepository;
        private readonly IAssignmentLinkRepository assignmentLinkRepository;
        private readonly ITopicRepository topicRepository;
        private readonly IRecomendationService recommendationService;
        private readonly IAssignmentService assignmentService;
        private readonly ITopicService topicService;
        private readonly IMapper mapper;

        public DailyAssignmentService(IUnitOfWork unitOfWork, IRecomendationService recommendationService, IAssignmentService assignmentService, ITopicService topicService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.assignmentRepository = unitOfWork.AssignmentRepository;
            this.assignmentSetRepository = unitOfWork.AssignmentSetRepository;
            this.assignmentLinkRepository = unitOfWork.AssignmentLinkRepository;
            this.topicRepository = unitOfWork.TopicRepository;
            this.recommendationService = recommendationService;
            this.assignmentService = assignmentService;
            this.topicService = topicService;
            this.mapper = mapper;
        }
        public async Task AddDailyAssignmentAsync(int userId)
        {
            var assignmentSetModel = new AssignmentSetModel
            {
                ObjectModelType = ObjectModelType.DailyModel,
                ObjectId = (await unitOfWork.SubjectRepository.GetAllAsync()).First(x => true).Id, // only math
            };
            var assignmentSetId = await AddDailyAssignmentSetAsync(assignmentSetModel, userId);
            await unitOfWork.SaveChangesAsync();
            var dailyAssignmentModel = new DailyAssignmentModel
            {
                UserId = userId,
                AssignmentSetId = assignmentSetId,
                CreationDate = DateTime.Now,
            };
            var dailyAssignment = mapper.Map<DailyAssignmentModel, DailyAssignment>(dailyAssignmentModel);
            await unitOfWork.DailyAssignmentRepository.AddAsync(dailyAssignment);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task<int> AddDailyAssignmentSetAsync(AssignmentSetModel model, int userId)
        {
            var assignmentSet = mapper.Map<AssignmentSetModel, AssignmentSet>(model);
            await assignmentSetRepository.AddAsync(assignmentSet);
            await unitOfWork.SaveChangesAsync();
            if (assignmentSet.ObjectType == ObjectType.Daily)
            {
                var topicIds = await recommendationService.GetRecommendedTopicIdsAsync(userId);
                bool isCorrect = await topicRepository.AreTopicIdsPresent(topicIds);
                if (topicIds.Count == 0 || !isCorrect)
                {
                    var topics = await topicService.GetAllAsync(); // incorrect if subjects aren't only math :(
                    topicIds = topics.Select(x => x.Id).ToList();
                }
                await GenerateDailyAssignmentSetAsync(assignmentSet, topicIds);
            }
            await unitOfWork.SaveChangesAsync();
            return assignmentSet.Id;
        }

        private async Task<int> GenerateDailyAssignmentSetAsync(AssignmentSet assignmentSet, List<int> topicIds)
        {
            var oneAnswerAssignmentsForTestCount = 1;
            var openAnswerAssignmentsForTestCount = 1;
            var matchComplianceAssignmentsForTestCount = 1;
            var chapterIds = (await unitOfWork.SubjectRepository.GetByIdWithDetailsAsync(assignmentSet.ObjectId)).Chapters.Select(x => x.Id);
            var materialIds = (await unitOfWork.MaterialRepository.GetAllAsync()).Where(x => topicIds.Contains(x.TopicId)).Select(x => x.Id);
            var assignmentsIds = (await assignmentLinkRepository.GetAllAsync()).Where(x => materialIds.Contains(x.ObjectId) && x.ObjectType == ObjectType.Material).Select(x => x.AssignmentId);
            var allAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList();
            var oneAnswerAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.OneAnswerAsssignment && assignmentsIds.Contains(x.Id)).ToArray();
            var openAnswerAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.OpenAnswerAssignment && assignmentsIds.Contains(x.Id)).ToArray();
            var matchComplianceAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.MatchComplianceAssignment && assignmentsIds.Contains(x.Id)).ToArray();

            Random rnd = new Random();
            rnd.Shuffle(oneAnswerAssignments);
            rnd.Shuffle(openAnswerAssignments);
            rnd.Shuffle(matchComplianceAssignments);
            var oneAnswerAssignmentsForTest = oneAnswerAssignments.Take(oneAnswerAssignmentsForTestCount).ToList();
            var openAnswerAssignmentsForTest = openAnswerAssignments.Take(openAnswerAssignmentsForTestCount).ToList();
            var matchComplianceAssignmentsForTest = matchComplianceAssignments.Take(matchComplianceAssignmentsForTestCount).ToList();
            var assignments = oneAnswerAssignmentsForTest.Concat(openAnswerAssignmentsForTest).Concat(matchComplianceAssignmentsForTest);

            var assignmentLinks = assignments.Select(x => new AssignmentLink { AssignmentId = x.Id, ObjectId = assignmentSet.Id, ObjectType = ObjectType.AssignmentSet });
            foreach (var assignmentLink in assignmentLinks)
            {
                await assignmentLinkRepository.AddAsync(assignmentLink);
            }
            await unitOfWork.SaveChangesAsync();
            return assignmentSet.Id;
        }

        public async Task<int> CountDailySetsToAdd(int userId)
        {
            var dailyAssignments = await unitOfWork.DailyAssignmentRepository.GetDailyAssignmentsByUserIdWithDetailsAsync(userId);
            if (dailyAssignments is null) return 10;
            var statistic = await unitOfWork.StatisticRepository.GetStatisticByUserIdAsync(userId);
            var assignmentSetProgressDetails = await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id);

            var notStartedDailyAssignments = dailyAssignments
                .Where(da => !assignmentSetProgressDetails
                .Select(aspd => aspd.AssignmentSetId)
                .Contains(da.AssignmentSetId));

            var count = 10 - notStartedDailyAssignments.Count();
            return count;
        }

        public async Task<AssignmentSetModel?> GetDailyAssignmentSetAsync(int userId)
        {
            var dailyAssignments = await unitOfWork.DailyAssignmentRepository.GetDailyAssignmentsByUserIdWithDetailsAsync(userId);
            var statistic = await unitOfWork.StatisticRepository.GetStatisticByUserIdAsync(userId);
            var assignmentSetProgressDetails = await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id);

            var notStartedDailyAssignments = dailyAssignments
                .Where(da => !assignmentSetProgressDetails
                .Select(aspd => aspd.AssignmentSetId)
                .Contains(da.AssignmentSetId));


            var notStarted = notStartedDailyAssignments.FirstOrDefault();
            if (notStarted == null)
                return null;

            var assignmentSetModel = await assignmentService.GetAssignmentSetByIdAsync(notStarted.AssignmentSetId);

            return assignmentSetModel;
        }

    }
}
