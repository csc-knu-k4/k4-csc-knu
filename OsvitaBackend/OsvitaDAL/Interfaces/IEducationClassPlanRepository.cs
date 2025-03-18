using System;
using OsvitaDAL.Entities;
using OsvitaDAL.Repositories;

namespace OsvitaDAL.Interfaces
{
	public interface IEducationClassPlanRepository : IRepository<EducationClassPlan>
    {
        Task<List<AssignmentSetPlanDetail>> GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(int id);
        Task<EducationClassPlan> GetEducationClassPlanByEducationClassIdWithDetailsAsync(int educationClassId);
        Task<TopicPlanDetail> GetTopicPlanDetailByEducationClassPlanIdAndTopicIdAsync(int id, int topicId);
        Task<List<TopicPlanDetail>> GetTopicPlanDetailsByEducationClassPlanIdAsync(int id);
        Task DeleteTopicPlanDetailByIdAsync(int id);
    }
}

