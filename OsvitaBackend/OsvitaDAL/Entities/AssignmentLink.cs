namespace OsvitaDAL.Entities
{
    public class AssignmentLink : BaseEntity
    {
        public int AssignmentId { get; set; }
        public int ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}
