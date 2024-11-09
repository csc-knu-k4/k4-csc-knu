using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaBLL.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public string ProblemDescription { get; set; }
        public string Explanation { get; set; }
        public TaskType? TaskType { get; set; }
        public int? ParentAssignmentId { get; set; }
    }
}
