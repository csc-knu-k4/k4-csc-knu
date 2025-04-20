using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using OsvitaBLL.Configurations;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;

namespace OsvitaBLL.Services
{
	public class EducationClassService : IEducationClassService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEducationClassRepository educationClassRepository;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;
        private readonly HostSettings settings;

        public EducationClassService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IOptions<HostSettings> settings)
        {
            this.unitOfWork = unitOfWork;
            this.educationClassRepository = unitOfWork.EducationClassRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.settings = settings.Value;
        }

        public async Task<int> AddAsync(EducationClassModel model)
        {
            var educationClass = mapper.Map<EducationClass>(model);
            educationClass.EducationClassPlan = new EducationClassPlan();
            await educationClassRepository.AddAsync(educationClass);
            await unitOfWork.SaveChangesAsync();
            return educationClass.Id;
        }

        public async Task DeleteAsync(EducationClassModel model)
        {
            await educationClassRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await educationClassRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationClassModel>> GetAllAsync()
        {
            var educationClasses = await educationClassRepository.GetAllWithDetailsAsync();
            var educationClassesModels = mapper.Map<IEnumerable<EducationClass>, IEnumerable<EducationClassModel>>(educationClasses);
            return educationClassesModels;
        }

        public async Task<EducationClassModel> GetByIdAsync(int id)
        {
            var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(id);
            var educationClassesModel = mapper.Map<EducationClass, EducationClassModel>(educationClass);
            return educationClassesModel;
        }

        public async Task UpdateAsync(EducationClassModel model)
        {
            var educationClass = mapper.Map<EducationClass>(model);
            await educationClassRepository.UpdateAsync(educationClass);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStudentByIdAsync(int id, int educationClassId)
        {
            var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(educationClassId);
            var userToDelete = educationClass.Students.FirstOrDefault(x => x.Id == id);
            if (userToDelete is not null)
            {
                educationClass.Students.Remove(userToDelete);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationClassModel>> GetByStudentIdAsync(int studentId)
        {
            var educationClasses = (await educationClassRepository.GetAllWithDetailsAsync()).Where(x => x.Students.Select(x => x.Id).Contains(studentId));
            var educationClassesModels = mapper.Map<IEnumerable<EducationClass>, IEnumerable<EducationClassModel>>(educationClasses);
            return educationClassesModels;
        }

        public async Task<IEnumerable<EducationClassModel>> GetByTeacherIdAsync(int teacherId)
        {
            var educationClasses = (await educationClassRepository.GetAllWithDetailsAsync()).Where(x => x.TeacherId == teacherId);
            var educationClassesModels = mapper.Map<IEnumerable<EducationClass>, IEnumerable<EducationClassModel>>(educationClasses);
            return educationClassesModels;
        }

        public async Task<EducationClassPlanVm> GetEducationClassPlanByEducationClassIdAsync(int id)
        {
            var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(id);
            var educationClassPlan = new EducationClassPlanVm
            {
                Id = educationClass.EducationClassPlan.Id,
                EducationClassId = educationClass.Id,
                EducationClassName = educationClass.Name,
                Topics = new List<TopicVm>(),
                AssignmentSets = new List<AssignmentSetVm>()
            };
            foreach (var topicPlanDetail in educationClass.EducationClassPlan.TopicPlanDetails)
            {
                var topicPlanVm = new TopicVm
                {
                    Id = topicPlanDetail.Id,
                    TopicId = topicPlanDetail.TopicId,
                    Title = topicPlanDetail.Topic.Title
                };
                educationClassPlan.Topics.Add(topicPlanVm);
            }
            foreach (var assignmentSetPlanDetail in educationClass.EducationClassPlan.AssignmentSetPlanDetails)
            {
                var assignmentSet = await unitOfWork.AssignmentSetRepository.GetByIdAsync(assignmentSetPlanDetail.AssignmentSetId);
                var title = await GetAssignmentSetTitleAsync(assignmentSet.ObjectId, assignmentSet.ObjectType);
                var assignmentSetVm = new AssignmentSetVm
                {
                    Id = assignmentSetPlanDetail.Id,
                    AssignmentSetId = assignmentSetPlanDetail.AssignmentSetId,
                    ObjectModelType = mapper.Map<ObjectModelType>(assignmentSet.ObjectType),
                    Title = title
                };
                educationClassPlan.AssignmentSets.Add(assignmentSetVm);
            }
            return educationClassPlan;
        }

        public async Task<IEnumerable<EducationClassPlanVm>> GetEducationClassPlansByStudentIdAsync(int studentId)
        {
            var educationClassPlans = new List<EducationClassPlanVm>();
            var educationClasses = (await educationClassRepository.GetAllWithDetailsAsync()).Where(x => x.Students.Select(x => x.Id).Contains(studentId));
            var statistic = await unitOfWork.StatisticRepository.GetStatisticByUserIdWithDetailsAsync(studentId);
            if (educationClasses is not null)
            {
                foreach (var educationClass in educationClasses)
                {
                    var educationClassPlan = new EducationClassPlanVm
                    {
                        Id = educationClass.EducationClassPlan.Id,
                        EducationClassId = educationClass.Id,
                        EducationClassName = educationClass.Name,
                        Topics = new List<TopicVm>(),
                        AssignmentSets = new List<AssignmentSetVm>()
                    };
                    foreach (var topicPlanDetail in educationClass.EducationClassPlan.TopicPlanDetails)
                    {
                        var topicPlanVm = new TopicVm
                        {
                            Id = topicPlanDetail.Id,
                            TopicId = topicPlanDetail.TopicId,
                            Title = topicPlanDetail.Topic.Title,
                            IsCompleted = statistic.TopicProgressDetails.Where(x => x.IsCompleted).Select(x => x.TopicId).Contains(topicPlanDetail.TopicId)
                        };
                        educationClassPlan.Topics.Add(topicPlanVm);
                    }
                    foreach (var assignmentSetPlanDetail in educationClass.EducationClassPlan.AssignmentSetPlanDetails)
                    {
                        var assignmentSet = await unitOfWork.AssignmentSetRepository.GetByIdAsync(assignmentSetPlanDetail.AssignmentSetId);
                        var title = await GetAssignmentSetTitleAsync(assignmentSet.ObjectId, assignmentSet.ObjectType);
                        var assignmentSetVm = new AssignmentSetVm
                        {
                            Id = assignmentSetPlanDetail.Id,
                            AssignmentSetId = assignmentSetPlanDetail.AssignmentSetId,
                            ObjectModelType = mapper.Map<ObjectModelType>(assignmentSet.ObjectType),
                            Title = title,
                            IsCompleted = statistic.AssignmentSetProgressDetails.Where(x => x.IsCompleted).Select(x => x.AssignmentSetId).Contains(assignmentSetPlanDetail.AssignmentSetId)
                        };
                        educationClassPlan.AssignmentSets.Add(assignmentSetVm);
                    }
                    educationClassPlans.Add(educationClassPlan);
                }
            }
            return educationClassPlans;
        }

        private async Task<string> GetAssignmentSetTitleAsync(int objectId, ObjectType objectType)
        {
            var title = string.Empty;
            if (objectType == ObjectType.Subject)
            {
                title = (await unitOfWork.SubjectRepository.GetByIdAsync(objectId)).Title;
            }
            if (objectType == ObjectType.Topic)
            {
                title = (await unitOfWork.TopicRepository.GetByIdAsync(objectId)).Title;
            }
            return title;
        }

        public async Task InviteStudentByEmailAsync(string email, int educationClassId)
        {
            var user = (await unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(x => x.Email == email);
            if (user is not null)
            {
                var invitation = new EducationClassInvitation
                {
                    Guid = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    EducationClassId = educationClassId,
                    CreatedDate = DateTime.Now
                };
                await educationClassRepository.AddEducationClassInvitationAsync(invitation);
                var subject = "Запрошення до навчального класу";
                var message = $"Щоб прийняти запрошення, перейдіть за посиланням: {settings.BaseUrl}/invitationAccept.html?id={educationClassId}&userId={user.Id}&guid={invitation.Guid}";
                await emailService.SendEmailAsync(email, subject, message);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ConfirmStudentAsync(int id, int educationClassId, string guid)
        {
            var invitation = await educationClassRepository.GetEducationClassInvitationByGuidAsync(guid);
            if (invitation is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(id);
                if (user is not null)
                {
                    var educationClass = await educationClassRepository.GetByIdWithDetailsAsync(educationClassId);
                    if (educationClass is not null && !educationClass.Students.Select(x => x.Id).Contains(user.Id))
                    {
                        educationClass.Students.Add(user);
                        await educationClassRepository.DeleteEducationClassInvitationByGuidAsync(guid);
                        await unitOfWork.SaveChangesAsync();
                    }
                }
            }
        }
    }
}

