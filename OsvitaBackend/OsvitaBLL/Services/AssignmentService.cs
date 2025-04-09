using AutoMapper;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Migrations;
using OsvitaDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OsvitaBLL.Services
{
    public partial class AssignmentService : IAssignmentService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IAssignmentSetRepository assignmentSetRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IAssignmentLinkRepository assignmentLinkRepository;
        private readonly IMapper mapper;

        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            assignmentRepository = unitOfWork.AssignmentRepository;
            assignmentSetRepository = unitOfWork.AssignmentSetRepository;
            answerRepository = unitOfWork.AnswerRepository;
            assignmentLinkRepository = unitOfWork.AssignmentLinkRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAssignmentAsync(AssignmentModel model)
        {
            int id = await AddOneAssignmentAsync(model);
            if (model.AssignmentModelType == AssignmentModelType.MatchComplianceAssignment)
            {
                foreach (var childAssignment in model.ChildAssignments)
                {
                    childAssignment.ParentAssignmentId = id;
                    childAssignment.ObjectId = null;
                    await AddOneAssignmentAsync(childAssignment);
                }
            }
            return id;
        }

        public async Task<IEnumerable<AssignmentModel>> GetAllAssignmentsAsync()
        {
            var assignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment);
            var assignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(assignments);
            foreach (var assignmentModel in assignmentsModels)
            {
                assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            }
            return assignmentsModels.OrderBy(x => x.AssignmentModelType).ThenBy(x => x.Id);
        }

        public async Task<IEnumerable<AssignmentModel>> GetAssignmentsByObjectIdAsync(int objectId, ObjectModelType objectModelType)
        {
            var objectType = mapper.Map<ObjectModelType, ObjectType>(objectModelType);
            var assignmentsIds = (await assignmentLinkRepository.GetAllAsync()).Where(x => x.ObjectId == objectId && x.ObjectType == objectType).Select(x => x.AssignmentId);
            var assignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment && assignmentsIds.Contains(x.Id));
            var assignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(assignments);
            foreach (var assignmentModel in assignmentsModels)
            {
                assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            }
            return assignmentsModels.OrderBy(x => x.AssignmentModelType).ThenBy(x => x.Id);
        }

        public async Task<AssignmentModel> GetAssignmentByIdAsync(int id)
        {
            var assignment = await assignmentRepository.GetByIdWithDetailsAsync(id);
            var assignmentModel = mapper.Map<Assignment, AssignmentModel>(assignment);
            assignmentModel.ChildAssignments = (await GetChildAssignmentsAsync(assignmentModel.Id)).ToList();
            return assignmentModel;
        }

        public async Task DeleteAssignmentByIdAsync(int id)
        {
            var assignment = await assignmentRepository.GetByIdAsync(id);
            if (assignment.AssignmentType == AssignmentType.MatchComplianceAssignment)
            {
                var childAssignments = await GetChildAssignmentsAsync(id);
                foreach (var childAssignment in childAssignments)
                {
                    await assignmentRepository.DeleteByIdAsync(childAssignment.Id);
                }
                await unitOfWork.SaveChangesAsync();
            }
            var assignmentLinks = (await assignmentLinkRepository.GetAllAsync()).Where(x => x.AssignmentId == id);
            foreach (var assignmentLink in assignmentLinks)
            {
                await assignmentLinkRepository.DeleteByIdAsync(assignmentLink.Id);
            }
            await unitOfWork.SaveChangesAsync();
            await assignmentRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAssignmentAsync(AssignmentModel model)
        {
            await UpdateOneAssignmentAsync(model);
            if (model.AssignmentModelType == AssignmentModelType.MatchComplianceAssignment)
            {
                foreach (var childAssignment in model.ChildAssignments)
                {
                    childAssignment.ParentAssignmentId = model.Id;
                    childAssignment.ObjectId = null;
                    var oldChildAssignmentsIds = (await assignmentRepository.GetAllAsync()).Where(x => x.ParentAssignmentId == childAssignment.ParentAssignmentId).Select(x => x.Id);
                    if (oldChildAssignmentsIds.Contains(childAssignment.Id))
                    {
                        await UpdateOneAssignmentAsync(childAssignment);
                    }
                    else
                    {
                        await AddOneAssignmentAsync(childAssignment);
                    }
                }
            }
        }

        public async Task<int> AddAssignmentSetAsync(AssignmentSetModel model)
        {
            var assignmentSet = mapper.Map<AssignmentSetModel, AssignmentSet>(model);
            await assignmentSetRepository.AddAsync(assignmentSet);
            await unitOfWork.SaveChangesAsync();
            if (assignmentSet.ObjectType == ObjectType.Topic)
            {
                await GenerateTopicAssignmentSetAsync(assignmentSet);
            }
            if (assignmentSet.ObjectType == ObjectType.Subject)
            {
                await GenerateSubjectAssignmentSetAsync(assignmentSet);
            }
            if (assignmentSet.ObjectType == ObjectType.Diagnostical)
            {
                await GenerateDiagnosticalAssignmentSetAsync(assignmentSet);
            }
            await unitOfWork.SaveChangesAsync();
            return assignmentSet.Id;
        }

        public async Task<AssignmentSetModel> GetAssignmentSetByIdAsync(int id)
        {
            var assignmentSet = await assignmentSetRepository.GetByIdAsync(id);
            var assignmentSetModel = mapper.Map<AssignmentSet, AssignmentSetModel>(assignmentSet);
            assignmentSetModel.Assignments = (await GetAssignmentsByObjectIdAsync(assignmentSetModel.Id, ObjectModelType.AssignmentSetModel)).OrderBy(x => x.AssignmentModelType).ThenBy(x => x.Id).ToList();
            return assignmentSetModel;
        }

        private async Task<IEnumerable<AssignmentModel>> GetChildAssignmentsAsync(int parentAssignmentId)
        {
            var childAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).Where(x => x.ParentAssignmentId == parentAssignmentId);
            var childAssignmentsModels = mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentModel>>(childAssignments);
            return childAssignmentsModels;
        }

        private async Task<int> AddOneAssignmentAsync(AssignmentModel model)
        {
            var assignment = mapper.Map<Assignment>(model);
            if (assignment.AssignmentType != AssignmentType.ChildAssignment)
            {
                assignment.ParentAssignmentId = null;
            }
            await assignmentRepository.AddAsync(assignment);
            await unitOfWork.SaveChangesAsync();
            if (model.ObjectId is not null)
            {
                var assignmentLink = new AssignmentLink { AssignmentId = assignment.Id, ObjectId = (int)model.ObjectId, ObjectType = ObjectType.Material };
                await assignmentLinkRepository.AddAsync(assignmentLink);
                await unitOfWork.SaveChangesAsync();
            }
            return assignment.Id;
        }

        private async Task UpdateOneAssignmentAsync(AssignmentModel model)
        {
            var assignment = mapper.Map<Assignment>(model);
            if (assignment.AssignmentType != AssignmentType.ChildAssignment)
            {
                assignment.ParentAssignmentId = null;
            }
            await assignmentRepository.UpdateAsync(assignment);
            await unitOfWork.SaveChangesAsync();

            var oldAssignmentAnswersIds = (await answerRepository.GetAllAsync()).Where(x => x.AssignmentId == assignment.Id).Select(x => x.Id);
            foreach (var oldAssignmentAnswerId in oldAssignmentAnswersIds)
            {
                if (!assignment.Answers.Select(x => x.Id).Contains(oldAssignmentAnswerId))
                {
                    await answerRepository.DeleteByIdAsync(oldAssignmentAnswerId);
                }
            }

            if (model.ObjectId is not null)
            {
                var assignmentLink = (await assignmentLinkRepository.GetAllAsync()).FirstOrDefault(x => x.AssignmentId == model.Id);
                if (assignmentLink is not null)
                {
                    assignmentLink.ObjectId = (int)model.ObjectId;
                    await assignmentLinkRepository.UpdateAsync(assignmentLink);
                    await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    assignmentLink = new AssignmentLink { AssignmentId = assignment.Id, ObjectId = (int)model.ObjectId, ObjectType = ObjectType.Material };
                    await assignmentLinkRepository.AddAsync(assignmentLink);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task<int> GenerateTopicAssignmentSetAsync(AssignmentSet assignmentSet)
        {
            var oneAnswerAssignmentsForTestCount = 5;
            var openAnswerAssignmentsForTestCount = 3;
            var matchComplianceAssignmentsForTestCount = 2;
            var materialIds = (await unitOfWork.MaterialRepository.GetAllAsync()).Where(x => x.TopicId == assignmentSet.ObjectId).Select(x => x.Id);
            var assignmentsIds = (await assignmentLinkRepository.GetAllAsync()).Where(x => materialIds.Contains(x.ObjectId) && x.ObjectType == ObjectType.Material).Select(x => x.AssignmentId);
            var allAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList().Where(x => x.AssignmentType != AssignmentType.ChildAssignment && assignmentsIds.Contains(x.Id));
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

        private async Task<int> GenerateSubjectAssignmentSetAsync(AssignmentSet assignmentSet)
        {
            var oneAnswerAssignmentsForTestCount = 15;
            var openAnswerAssignmentsForTestCount = 3;
            var matchComplianceAssignmentsForTestCount = 4;
            var chapterIds = (await unitOfWork.SubjectRepository.GetByIdWithDetailsAsync(assignmentSet.ObjectId)).Chapters.Select(x => x.Id);
            var topicIds = (await unitOfWork.TopicRepository.GetAllAsync()).Where(x => chapterIds.Contains(x.ChapterId)).Select(x => x.Id);
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

        private async Task<int> GenerateDiagnosticalAssignmentSetAsync(AssignmentSet assignmentSet)
        {
            Random rnd = new Random();
            var oneAnswerAssignmentsForTestCountPerTopic = 3;
            var openAnswerAssignmentsForTestCountPerTopic = 1;
            var matchComplianceAssignmentsForTestCountPerTopic = 1;
            var chapterIds = (await unitOfWork.SubjectRepository.GetByIdWithDetailsAsync(assignmentSet.ObjectId)).Chapters.Select(x => x.Id);
            var topicIds = (await unitOfWork.TopicRepository.GetAllAsync()).Where(x => chapterIds.Contains(x.ChapterId)).Select(x => x.Id);
            var allAssignments = (await assignmentRepository.GetAllWithDetailsAsync()).ToList();
            var assignments = new List<Assignment>();
            foreach (var topicId in topicIds)
            {
                var materialIds = (await unitOfWork.MaterialRepository.GetAllAsync()).Where(x => x.TopicId == topicId).Select(x => x.Id);
                var assignmentsIds = (await assignmentLinkRepository.GetAllAsync()).Where(x => materialIds.Contains(x.ObjectId) && x.ObjectType == ObjectType.Material).Select(x => x.AssignmentId);
                var oneAnswerAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.OneAnswerAsssignment && assignmentsIds.Contains(x.Id)).ToArray();
                var openAnswerAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.OpenAnswerAssignment && assignmentsIds.Contains(x.Id)).ToArray();
                var matchComplianceAssignments = allAssignments.Where(x => x.AssignmentType == AssignmentType.MatchComplianceAssignment && assignmentsIds.Contains(x.Id)).ToArray();
                var oneAnswerAssignmentsForTest = new List<Assignment>();
                var openAnswerAssignmentsForTest = new List<Assignment>();
                var matchComplianceAssignmentsForTest = new List<Assignment>();
                if (oneAnswerAssignments.Count() > 0)
                {
                    rnd.Shuffle(oneAnswerAssignments);
                    oneAnswerAssignmentsForTest = oneAnswerAssignments.Take(oneAnswerAssignmentsForTestCountPerTopic).ToList();
                }
                if (openAnswerAssignments.Count() > 0)
                {
                    rnd.Shuffle(openAnswerAssignments);
                    openAnswerAssignmentsForTest = openAnswerAssignments.Take(openAnswerAssignmentsForTestCountPerTopic).ToList();
                }
                if (matchComplianceAssignments.Count() > 0)
                {
                    rnd.Shuffle(matchComplianceAssignments);
                    matchComplianceAssignmentsForTest = matchComplianceAssignments.Take(matchComplianceAssignmentsForTestCountPerTopic).ToList();
                }
                assignments.AddRange(oneAnswerAssignmentsForTest.Concat(openAnswerAssignmentsForTest).Concat(matchComplianceAssignmentsForTest));
            }

            var assignmentLinks = assignments.Select(x => new AssignmentLink { AssignmentId = x.Id, ObjectId = assignmentSet.Id, ObjectType = ObjectType.AssignmentSet });
            foreach (var assignmentLink in assignmentLinks)
            {
                await assignmentLinkRepository.AddAsync(assignmentLink);
            }
            await unitOfWork.SaveChangesAsync();
            return assignmentSet.Id;
        }

        public async Task AddDailyAssignmentAsync(int userId)
        {
            var assignmentSet = new AssignmentSet
            {
                ObjectType = ObjectType.Daily,
                ObjectId = (await unitOfWork.SubjectRepository.GetAllAsync()).First(x => true).Id, // only math
            };
            var assignmentSetId = await GenerateDailyAssignmentSetAsync(assignmentSet);
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

        private async Task<int> GenerateDailyAssignmentSetAsync(AssignmentSet assignmentSet)
        {
            var oneAnswerAssignmentsForTestCount = 1;
            var openAnswerAssignmentsForTestCount = 1;
            var matchComplianceAssignmentsForTestCount = 1;
            var chapterIds = (await unitOfWork.SubjectRepository.GetByIdWithDetailsAsync(assignmentSet.ObjectId)).Chapters.Select(x => x.Id);
            var topicIds = (await unitOfWork.TopicRepository.GetAllAsync()).Where(x => chapterIds.Contains(x.ChapterId)).Select(x => x.Id);
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

        public async Task<AssignmentSetModel> GetDailyAssignmentSetAsync(int userId)
        {
            var dailyAssignments = await unitOfWork.DailyAssignmentRepository.GetDailyAssignmentsByUserIdWithDetailsAsync(userId);
            var statistic = await unitOfWork.StatisticRepository.GetStatisticByUserIdAsync(userId);
            var assignmentSetProgressDetails = await unitOfWork.StatisticRepository.GetAssignmentSetProgressDetailsByStatisticIdAsync(statistic.Id);
            var notStartedAssignmentSetId = from da in dailyAssignments
                                       where !(from aspd in assignmentSetProgressDetails
                                               select aspd.AssignmentSetId)
                                               .Contains(da.AssignmentSetId)
                                       select da;
            var assignmentSet = await unitOfWork.AssignmentSetRepository.GetByIdAsync(notStartedAssignmentSetId.First(t => true).AssignmentSetId);
            var assignmentSetModel = mapper.Map<AssignmentSet, AssignmentSetModel>(assignmentSet);
            return assignmentSetModel;
        }
    }
}
