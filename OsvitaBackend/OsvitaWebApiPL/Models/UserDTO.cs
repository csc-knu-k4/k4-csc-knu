using System;
using System.ComponentModel.DataAnnotations;

namespace OsvitaWebApiPL.Models
{
	public class UserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}

