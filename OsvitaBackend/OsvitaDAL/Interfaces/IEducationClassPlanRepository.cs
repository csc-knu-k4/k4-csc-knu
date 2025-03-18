using System;
using OsvitaDAL.Entities;
using OsvitaDAL.Repositories;

namespace OsvitaDAL.Interfaces
{
	public interface IEducationClassPlanRepository : IRepository<EducationClassPlan>
    {
        Task<List<AssignmentSetPlanDetail>> GetAssignmentSetPlanDetailsByEducationClassPlanIdAsync(int id);
        Task<EducationClassPlan> GetEducationClassPlanByEducationClassIdWithDetailsAsync(int educationClassId);
        Task<TopicPlanDetail> GetTopicPlanDetailByEducationClassPlanIdAndTopicIdAsync(int educationClassPlanId, int topicId);
        Task DeleteTopicPlanDetailByIdAsync(int id);
    }
}

