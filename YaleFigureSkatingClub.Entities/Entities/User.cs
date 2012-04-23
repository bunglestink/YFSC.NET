using System;
//using System.Security.Principal;
using System.Collections.Generic;

namespace YaleFigureSkatingClub.Entities
{
	public class User
	{
		public virtual string Username { get; set; }
		public virtual string Password { get; set; }
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

