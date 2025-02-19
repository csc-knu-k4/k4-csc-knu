namespace OsvitaBLL.Models
{
	public class UserModel
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public int? StatisticModelId { get; set; }
    }
}

