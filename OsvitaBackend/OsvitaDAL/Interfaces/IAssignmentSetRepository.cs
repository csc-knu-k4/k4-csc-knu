using System;
using OsvitaDAL.Entities;

namespace OsvitaDAL.Interfaces
{
	public interface IAssignmentSetRepository : IRepository<AssignmentSet>
	{
        //Task<DailyAssignment> GetDailyAssignmentByIdAsync(int Id);
        //Task<IEnumerable<DailyAssignment>> GetAllDailyAssignmentsByUserIdAsync(int userId);
    }
}

