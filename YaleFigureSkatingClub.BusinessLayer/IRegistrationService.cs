using System;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public interface IRegistrationService
	{
		void SubmitRegistration(AnnualRegistration annualRegistration);
		decimal GetRegistrationCost(AnnualRegistration annualRegistration);
	}
}

