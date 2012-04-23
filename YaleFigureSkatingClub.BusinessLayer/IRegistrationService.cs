using System;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public interface IRegistrationService
	{
		AnnualRegistration CreateNewAnnualRegistration();
		void SubmitRegistration(AnnualRegistration annualRegistration, User user);
		decimal GetRegistrationCost(AnnualRegistration annualRegistration);
	}
}

