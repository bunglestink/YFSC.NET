using System;
using System.Web;
using NHibernate;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.Infrastructure
{
	/// <summary>
	/// NH session manager.
	/// 
	/// Idea for this pattern from: 
	/// http://stackoverflow.com/questions/2046168/how-can-i-implement-a-robust-session-per-request-pattern-in-my-project-while-fo
	/// </summary>
	public static class NHSessionManager
	{
		// single SessionFactory for the application
		static ISessionFactory sessionFactory;
		
		/// <summary>
		/// Initializes the <see cref="YaleFigureSkatingClub.Models.NHSessionManager"/> class.
		/// </summary>
		static NHSessionManager()
		{
			NHConfiguration cfg = new NHConfiguration();
			sessionFactory = cfg.CreateSessionFactory();
		}
		
		/// <summary>
		/// Gets the session.
		/// </summary>
		public static ISession CurrentSession
		{
			get
			{
		        ISession session = HttpContext.Current.Items["NHibernateSession"] as ISession;
		        if (session == null) {
					session = sessionFactory.OpenSession();
		            HttpContext.Current.Items.Add("NHibernateSession", session);
		        }
		        return session;
			}
	    }
		
		
		/// <summary>
		/// Closes the session.
		/// </summary>
	    public static void CloseSession()
	    {
	        ISession session = HttpContext.Current.Items["NHibernateSession"] as ISession;
	        if (session != null) {
	            if (session.IsOpen) {
					if (session.Transaction != null && session.Transaction.IsActive) {
						try {
							session.Transaction.Commit();
						}
						catch (Exception) {
							session.Transaction.Rollback();
						}
					}
	                session.Flush();
	            }
	            HttpContext.Current.Items.Remove("NHibernateSession");
	        }
	    }
		
	}
}

