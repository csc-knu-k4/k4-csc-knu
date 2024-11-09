using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsvitaBLL.Models
{
    public class AssignmentLinkModel
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}
