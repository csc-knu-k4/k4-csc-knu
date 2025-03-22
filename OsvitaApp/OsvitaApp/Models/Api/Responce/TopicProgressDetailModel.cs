namespace OsvitaApp.Models.Api.Responce
{
    public class TopicProgressDetailModel
    {
        public int Id { get; set; }
        public int StatisticId { get; set; }
        public int TopicId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
    }
}