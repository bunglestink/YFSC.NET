using System;
using System.Linq;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public class RegistrationService
	{
		public RegistrationService ()
		{
		}
		
		
		public decimal GetRegistrationCost(AnnualRegistration registration)
		{
			return registration.Skaters
				.SelectMany(s => s.Sessions)
				.Count();
		}
	}
}

