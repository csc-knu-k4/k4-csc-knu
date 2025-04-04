using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaDAL.Entities;
using OsvitaDAL.Interfaces;
using OsvitaDAL.Repositories;

namespace OsvitaBLL.Services
{
    public class EducationPlanService : IEducationPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEducationPlanRepository educationPlanRepository;
        private readonly IMapper mapper;

        public EducationPlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            educationPlanRepository = unitOfWork.EducationPlanRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddAssignmentSetPlanDetailAsync(AssignmentSetPlanDetailModel model, int userId)
        {
            var assignmentSetPlanDetail = mapper.Map<AssignmentSetPlanDetail>(model);
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            assignmentSetPlanDetail.EducationClassPlanId = null;
            educationPlan.AssignmentSetPlanDetails.Add(assignmentSetPlanDetail);
            await educationPlanRepository.UpdateAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
            return assignmentSetPlanDetail.Id;
        }

        public async Task<int> AddAsync(EducationPlanModel model)
        {
            var educationPlan = mapper.Map<EducationPlan>(model);
            await educationPlanRepository.AddAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
            return educationPlan.Id;
        }

        public async Task<int> AddTopicPlanDetailAsync(TopicPlanDetailModel model, int userId)
        {
            var topicPlanDetail = mapper.Map<TopicPlanDetail>(model);
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            if (educationPlan.TopicPlanDetails is null)
            {
                educationPlan.TopicPlanDetails = new List<TopicPlanDetail>();
            }
            topicPlanDetail.EducationClassPlanId = null;
            educationPlan.TopicPlanDetails.Add(topicPlanDetail);
            await educationPlanRepository.UpdateAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
            return topicPlanDetail.Id;
        }

        public async Task<int> DeleteAssignmentSetPlanDetailAsync(int userId, int assignmentSetId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            var assignmentSetPlanDetail = await educationPlanRepository.GetAssignmentSetPlanDetailByEducationPlanIdAndAssignmentSetIdAsync(educationPlan.Id, assignmentSetId);
            await educationPlanRepository.DeleteAssignmentSetPlanDetailByIdAsync(assignmentSetPlanDetail.Id);
            await unitOfWork.SaveChangesAsync();
            return assignmentSetPlanDetail.Id;
        }

        public async Task DeleteAsync(EducationPlanModel model)
        {
            await educationPlanRepository.DeleteByIdAsync(model.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await educationPlanRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteTopicPlanDetailAsync(int userId, int topicId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            var topicPlanDetail = await educationPlanRepository.GetTopicPlanDetailByEducationPlanIdAndTopicIdAsync(educationPlan.Id, topicId);
            await educationPlanRepository.DeleteTopicPlanDetailByIdAsync(topicPlanDetail.Id);
            await unitOfWork.SaveChangesAsync();
            return topicPlanDetail.Id;
        }

        public Task<IEnumerable<EducationPlanModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EducationPlanModel> GetByIdAsync(int id)
        {
            var educationPlan = await educationPlanRepository.GetByIdAsync(id);
            educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id)).ToList();
            var educationPlanModel = mapper.Map<EducationPlan, EducationPlanModel>(educationPlan);
            return educationPlanModel;
        }

        public async Task<EducationPlanModel> GetEducationPlanByUserIdAsync(int userId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id));
            var educationPlanModel = mapper.Map<EducationPlan, EducationPlanModel>(educationPlan);
            return educationPlanModel;
        }

        public async Task<TopicPlanDetailModel> GetTopicPlanDetailAsync(int userId, int topicId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdAsync(userId);
            var topicPlanDetail = await educationPlanRepository.GetTopicPlanDetailByEducationPlanIdAndTopicIdAsync(educationPlan.Id, topicId);
            var topicPlanDetailModel = mapper.Map<TopicPlanDetail, TopicPlanDetailModel>(topicPlanDetail);
            return topicPlanDetailModel;
        }

        public async Task UpdateAsync(EducationPlanModel model)
        {
            var educationPlan = mapper.Map<EducationPlan>(model);
            await educationPlanRepository.UpdateAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateTopicPlanDetailAsync(TopicPlanDetailModel model, int userId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            var oldTopicPlanDetail = educationPlan.TopicPlanDetails.FirstOrDefault(x => x.TopicId == model.TopicId);
            // some changes
            await unitOfWork.SaveChangesAsync();
            return oldTopicPlanDetail.Id;
        }
    }
}
