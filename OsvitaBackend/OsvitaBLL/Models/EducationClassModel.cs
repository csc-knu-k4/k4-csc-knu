using System;
using OsvitaDAL.Entities;

namespace OsvitaBLL.Models
{
	public class EducationClassModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public List<int> StudentsIds { get; set; }
    }
}

