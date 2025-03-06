using AutoMapper;
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
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdAsync(userId);
            educationPlan.TopicPlanDetails.Add(topicPlanDetail);
            await educationPlanRepository.UpdateAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
            return topicPlanDetail.Id;
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

        public async Task DeleteTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId)
        {
            var topicPlanDetail = educationPlanRepository.DeleteTopicPlanDetailByUserIdAndTopicIdAsync(userId, topicId);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationPlanModel>> GetAllAsync()
        {
            var educationPlans = await educationPlanRepository.GetAllAsync();
            foreach (var educationPlan in educationPlans)
            {
                educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id)).ToList();
            }
            var educationPlansModels = mapper.Map<IEnumerable<EducationPlan>, IEnumerable<EducationPlanModel>>(educationPlans);
            return educationPlansModels;
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
            educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id)).ToList();
            var educationPlanModel = mapper.Map<EducationPlan, EducationPlanModel>(educationPlan);
            return educationPlanModel;
        }

        public async Task<TopicPlanDetailModel> GetTopicPlanDetailByUserIdAndTopicIdAsync(int userId, int topicId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id)).ToList();
            var topicPlanDetail = educationPlan.TopicPlanDetails.SingleOrDefault(x => x.TopicId == topicId);
            var topicPlanDetailModel = mapper.Map<TopicPlanDetail, TopicPlanDetailModel>(topicPlanDetail);
            return topicPlanDetailModel;
        }

        public async Task UpdateAsync(EducationPlanModel model)
        {
            var educationPlan = mapper.Map<EducationPlan>(model);
            await educationPlanRepository.UpdateAsync(educationPlan);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateTopicPlanDetailAsync(TopicPlanDetailModel model, int userId, int topicId)
        {
            var educationPlan = await educationPlanRepository.GetEducationPlanByUserIdWithDetailsAsync(userId);
            educationPlan.TopicPlanDetails = (await unitOfWork.EducationPlanRepository.GetTopicPlanDetailsByEducationPlanIdAsync(educationPlan.Id)).ToList();
            var oldTopicPlanDetail = educationPlan.TopicPlanDetails.SingleOrDefault(x => x.TopicId == topicId);
            // changes here
            await unitOfWork.SaveChangesAsync();
            return oldTopicPlanDetail.Id;
        }
    }
}
