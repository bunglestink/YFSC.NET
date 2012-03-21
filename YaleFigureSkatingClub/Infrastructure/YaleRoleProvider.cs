using System;
using System.Collections.Generic;
using System.Web.ApplicationServices;
using System.Web.Security;
using System.Linq;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace YaleFigureSkatingClub
{
	public class YaleRoleProvider : RoleProvider
	{
		public override string[] GetRolesForUser (string username)
		{
			ISession session = NHSessionManager.CurrentSession;
			session.BeginTransaction();
			return  session
						.Query<Role>()
						.Where (r => r.Username == username)
						.Select (r => r.Rolename)
						.ToArray();
		}
		
		
		#region implemented abstract members of System.Web.Security.RoleProvider
		public override bool IsUserInRole (string username, string roleName)
		{
			throw new NotImplementedException ();
		}

		public override void CreateRole (string roleName)
		{
			throw new NotImplementedException ();
		}

		public override bool DeleteRole (string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException ();
		}

		public override bool RoleExists (string roleName)
		{
			throw new NotImplementedException ();
		}

		public override void AddUsersToRoles (string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException ();
		}

		public override void RemoveUsersFromRoles (string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException ();
		}

		public override string[] GetUsersInRole (string roleName)
		{
			throw new NotImplementedException ();
		}

		public override string[] GetAllRoles ()
		{
			throw new NotImplementedException ();
		}

		public override string[] FindUsersInRole (string roleName, string usernameToMatch)
		{
			throw new NotImplementedException ();
		}

		public override string ApplicationName {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

