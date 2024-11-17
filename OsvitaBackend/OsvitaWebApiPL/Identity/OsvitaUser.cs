using System;
using Microsoft.AspNetCore.Identity;

namespace OsvitaWebApiPL.Identity
{
	public class OsvitaUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? SecondName { get; set; }
	}
}

