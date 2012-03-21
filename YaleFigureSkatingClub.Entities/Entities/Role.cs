using System;

namespace YaleFigureSkatingClub.Entities
{
	public class Role
	{
		public virtual int ID { get; set; }
		public virtual string Username { get; set; }
		public virtual string Rolename { get; set; }
		
		public virtual User User { get; set; }
		
		public Role ()
		{
		}
	}
}

