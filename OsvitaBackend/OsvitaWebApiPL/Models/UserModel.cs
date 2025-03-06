using System;
using System.ComponentModel.DataAnnotations;

namespace OsvitaWebApiPL.Models
{
	public class UserModel
	{
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<string> Roles { get; set; }
    }
}

