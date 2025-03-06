using System;
namespace OsvitaDAL.Entities
{
	public class EducationClass : BaseEntity
	{
		public string Name { get; set; }
		public int TeacherId { get; set; }
        public User Teacher { get; set; }
        public List<User> Students { get; set; }
	}
}

