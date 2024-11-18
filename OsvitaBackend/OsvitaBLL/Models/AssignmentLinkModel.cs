namespace OsvitaBLL.Models
{
    public class AssignmentLinkModel
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int ObjectId { get; set; }
        public ObjectModelType ObjectType { get; set; }
    }
}
