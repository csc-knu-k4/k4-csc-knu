using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaDAL.Entities
{
    public class AssignmentLink : BaseEntity
    {
        public Assignment Assignment { get; set; }
        public BaseEntity Object { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}
