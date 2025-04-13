using OsvitaDAL.Entities;

namespace OsvitaBLL.Models
{
    public class DailyAssignmentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssignmentSetId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
