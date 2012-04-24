using System;
//using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace YaleFigureSkatingClub.Entities
{
	public class User
	{
		[Required]
		public virtual string Username { get; set; }
		
		[Required]
		public virtual string Password { get; set; }
		
		[Required]
		public virtual string Email { get; set; }
		
		public virtual IList<Role> Roles { get; set; }
		public virtual IList<AnnualRegistration> Registrations { get; set; }
		
		public User ()
		{
			Roles = new List<Role>();
			Registrations = new List<AnnualRegistration>();
		}
	}
}

