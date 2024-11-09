using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaDAL.Entities
{
    public class Assignment : BaseEntity
    {
        public string ProblemDescription { get; set; }
        public string Explanation { get; set; }
        public TaskType? AssignmentType { get; set; }
        public int? ParentAssignmentId { get; set; }

        public Assignment? ParentAssignment { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
