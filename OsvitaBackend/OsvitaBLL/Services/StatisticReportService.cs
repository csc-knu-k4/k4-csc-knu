using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Models.ReportModels;
using OsvitaDAL.Interfaces;
using QuestPDF.Fluent;
using OsvitaDAL.Entities;

namespace OsvitaBLL.Services
{
    public class StatisticReportService : IStatisticReportService
    {
        private readonly IStatisticService statisticService;
        private readonly IAssignmentService assignmentService;
        private readonly IUnitOfWork unitOfWork;
        public StatisticReportService(IStatisticService statisticService, IUnitOfWork unitOfWork, IAssignmentService assignmentService)
        {
            this.statisticService = statisticService;
            this.unitOfWork = unitOfWork;
            this.assignmentService = assignmentService;
        }

        public async Task<byte[]> GenerateAssignmetSetsReportAsync(int userId, int assignmentSetProgressDetailId)
        {
            var models = new List<AssignmentSetReportModel>();
            if (assignmentSetProgressDetailId > 0)
            {
                var model = await GetAssignmetSetReportModelAsync(userId, assignmentSetProgressDetailId);
                models.Add(model);
            }
            else
            {
                var statistic = await statisticService.GetStatisticByUserIdAsync(userId);
                var assignmentSetProgressDetailsIds = statistic.AssignmentSetProgressDetails.Where(x => x.IsCompleted).Select(x => x.Id).ToList();
                foreach (var id in assignmentSetProgressDetailsIds)
                {
                    var model = await GetAssignmetSetReportModelAsync(userId, id);
                    models.Add(model);
                }
            }
            var document = new UserAssignmentsReportDocument(models);
            var report = document.GeneratePdf();
            return report;
        }

        public async Task<byte[]> GenerateEducationClassAssignmetSetsReportAsync(int educationClassId, int assignmentSetId)
        {
            var models = new List<EducationClassAssignmetSetReportModel>();
            if (assignmentSetId > 0)
            {
                var model = await GetEducationClassAssignmetSetReportModelAsync(educationClassId, assignmentSetId);
                models.Add(model);
            }
            else
            {
                var educationPlan = await unitOfWork.EducationClassPlanRepository.GetEducationClassPlanByEducationClassIdWithDetailsAsync(educationClassId);
                var assignmentSetIds = educationPlan.AssignmentSetPlanDetails.Select(x => x.AssignmentSetId).ToList();
                foreach (var id in assignmentSetIds)
                {
                    var model = await GetEducationClassAssignmetSetReportModelAsync(educationClassId, id);
                    models.Add(model);
                }
            }
            var document = new EducationClassAssignmentsReportDocument(models);
            var report = document.GeneratePdf();
            return report;
        }

        private async Task<AssignmentSetReportModel> GetAssignmetSetReportModelAsync(int userId, int assignmentSetProgressDetailId)
        {
            var assignmentSetReportModel = new AssignmentSetReportModel();
            var statistic = await statisticService.GetStatisticByUserIdAsync(userId);
            var assignmentSet = await unitOfWork.AssignmentSetRepository.GetByIdAsync(statistic.AssignmentSetProgressDetails.FirstOrDefault(x => x.Id == assignmentSetProgressDetailId).AssignmentSetId);
            var assignmentSetProgress = statistic.AssignmentSetProgressDetails.FirstOrDefault(x => x.Id == assignmentSetProgressDetailId);
            if (assignmentSet is not null && assignmentSetProgress is not null && assignmentSetProgress.IsCompleted)
            {
                if (assignmentSet.ObjectType == ObjectType.Topic)
                {
                    var topic = await unitOfWork.TopicRepository.GetByIdAsync(assignmentSet.ObjectId);
                    assignmentSetReportModel = new AssignmentSetReportModel
                    {
                        UserId = userId,
                        ObjectId = topic.Id,
                        ObjectName = topic.Title,
                        ObjectType = ObjectModelType.TopicModel,
                        CompletedDate = assignmentSetProgress.CompletedDate,
                        Score = assignmentSetProgress.Score,
                        MaxScore = assignmentSetProgress.MaxScore,
                        Assignments = await GetAssignmetReportModelsAsync(assignmentSet.Id, ObjectModelType.AssignmentSetModel, assignmentSetProgress.AssignmentProgressDetails)
                    };
                }
                if (assignmentSet.ObjectType == ObjectType.Subject)
                {
                    var subject = await unitOfWork.SubjectRepository.GetByIdAsync(assignmentSet.ObjectId);
                    assignmentSetReportModel = new AssignmentSetReportModel
                    {
                        UserId = userId,
                        ObjectId = subject.Id,
                        ObjectName = subject.Title,
                        ObjectType = ObjectModelType.SubjectModel,
                        CompletedDate = assignmentSetProgress.CompletedDate,
                        Score = assignmentSetProgress.Score,
                        MaxScore = assignmentSetProgress.MaxScore,
                        Assignments = await GetAssignmetReportModelsAsync(assignmentSet.Id, ObjectModelType.AssignmentSetModel, assignmentSetProgress.AssignmentProgressDetails)
                    };
                }
                if (assignmentSet.ObjectType == ObjectType.Diagnostical)
                {
                    var subject = await unitOfWork.SubjectRepository.GetByIdAsync(assignmentSet.ObjectId);
                    assignmentSetReportModel = new AssignmentSetReportModel
                    {
                        UserId = userId,
                        ObjectId = subject.Id,
                        ObjectName = subject.Title,
                        ObjectType = ObjectModelType.DiagnosticalModel,
                        CompletedDate = assignmentSetProgress.CompletedDate,
                        Score = assignmentSetProgress.Score,
                        MaxScore = assignmentSetProgress.MaxScore,
                        Assignments = await GetAssignmetReportModelsAsync(assignmentSet.Id, ObjectModelType.AssignmentSetModel, assignmentSetProgress.AssignmentProgressDetails)
                    };
                }

            }
            return assignmentSetReportModel;
        }

        private async Task<List<AssignmentReportModel>> GetAssignmetReportModelsAsync(int objectId, ObjectModelType objectModelType, List<AssignmentProgressDetailModel> assignmentProgresses)
        {
            var assignments = (await assignmentService.GetAssignmentsByObjectIdAsync(objectId, objectModelType)).OrderBy(x => x.AssignmentModelType).ThenBy(x => x.Id);
            var assignmetReportModels = new List<AssignmentReportModel>();
            int assignmentNumber = 1;
            foreach (var assignment in assignments)
            {
                if (assignment.AssignmentModelType == AssignmentModelType.OneAnswerAsssignment || assignment.AssignmentModelType == AssignmentModelType.OpenAnswerAssignment)
                {
                    var materialId = (await unitOfWork.AssignmentLinkRepository.GetAllAsync()).FirstOrDefault(x => x.AssignmentId == assignment.Id && x.ObjectType == ObjectType.Material).ObjectId;
                    var topic = (await unitOfWork.MaterialRepository.GetByIdWithDetailsAsync(materialId)).Topic;
                    var assignmentProgress = assignmentProgresses.FirstOrDefault(x => x.AssignmentId == assignment.Id);
                    var assignmentReportModel = new AssignmentReportModel
                    {
                        AssignmentId = assignment.Id,
                        AssignmentNumber = assignmentNumber,
                        AssignmentType = assignment.AssignmentModelType,
                        TopicName = topic.Title,
                        IsCorrect = assignmentProgress.IsCorrect,
                        Points = assignmentProgress.Points,
                        MaxPoints = assignmentProgress.MaxPoints
                    };
                    assignmetReportModels.Add(assignmentReportModel);
                }
                if (assignment.AssignmentModelType == AssignmentModelType.MatchComplianceAssignment)
                {
                    var materialId = (await unitOfWork.AssignmentLinkRepository.GetAllAsync()).FirstOrDefault(x => x.AssignmentId == assignment.Id && x.ObjectType == ObjectType.Material).ObjectId;
                    var topic = (await unitOfWork.MaterialRepository.GetByIdWithDetailsAsync(materialId)).Topic;
                    var assignmentChildProgresses = assignmentProgresses.Where(x => assignment.ChildAssignments.Select(ca => ca.Id).Contains(x.AssignmentId));
                    var isCorrect = assignmentChildProgresses.All(x => x.IsCorrect);
                    var points = assignmentChildProgresses.Where(x => x.IsCorrect).Sum(x => x.Points);
                    var maxPoints = assignmentChildProgresses.Sum(x => x.MaxPoints);
                    var assignmentReportModel = new AssignmentReportModel
                    {
                        AssignmentId = assignment.Id,
                        AssignmentNumber = assignmentNumber,
                        AssignmentType = assignment.AssignmentModelType,
                        TopicName = topic.Title,
                        IsCorrect = isCorrect,
                        Points = points,
                        MaxPoints = maxPoints
                    };
                    assignmetReportModels.Add(assignmentReportModel);
                }
                assignmentNumber++;
            }
            return assignmetReportModels;
        }

        private async Task<EducationClassAssignmetSetReportModel> GetEducationClassAssignmetSetReportModelAsync(int educationClassId, int assignmentSetId)
        {
            var educationClass = await unitOfWork.EducationClassRepository.GetByIdWithDetailsAsync(educationClassId);
            var educationClassAssignmetSetReportModel = new EducationClassAssignmetSetReportModel
            {
                AssignmetSetReportModels = new List<AssignmentSetReportModel>()
            };
            foreach (var user in educationClass.Students)
            {
                var statistic = await statisticService.GetStatisticByUserIdAsync(user.Id);
                var assignmentSetProgressDetail = statistic.AssignmentSetProgressDetails.OrderByDescending(x => x.CompletedDate).FirstOrDefault(x => x.IsCompleted && x.AssignmentSetId == assignmentSetId);
                if (assignmentSetProgressDetail is not null)
                {
                    var model = await GetAssignmetSetReportModelAsync(user.Id, assignmentSetProgressDetail.Id);
                    model.UserFirstName = user.FirstName;
                    model.UserSecondName = user.SecondName;
                    model.UserEmail = user.Email;
                    educationClassAssignmetSetReportModel.AssignmetSetReportModels.Add(model);
                }
            }

            return educationClassAssignmetSetReportModel;
        }
    }
}

