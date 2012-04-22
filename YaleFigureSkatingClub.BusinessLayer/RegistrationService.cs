using System;
using System.Linq;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public class RegistrationService : IRegistrationService
	{
		public RegistrationService ()
		{
		}
		
		
		public void SubmitRegistration (AnnualRegistration registration) {
			// TODO: implement this.
		}
		
		public decimal GetRegistrationCost(AnnualRegistration registration)
		{
			var membershipCost = GetMembershipCost(registration);
			var homeClubCost = GetHomeClubCost(registration);
			var sessionCost = GetSessionCost(registration);
		
			return membershipCost + homeClubCost + sessionCost;
		}
		
		decimal GetMembershipCost(AnnualRegistration registration)
		{
			// Yale Affiliated: $20/member
			// Non-Yale: $30 first, $20 additional
			var skaters = registration.Skaters.Count();
			
			return registration.YaleAffiliation ?
				skaters * 20 :
				skaters == 0 ? 
					0 : 
					(skaters * 20) + 10;
		}
		
		decimal GetHomeClubCost(AnnualRegistration registration)
		{
			// Basic: $12 each, $12 for SnoplowSam parent
			// Regular: $50 first, $20 additional
			
			decimal total = 0;
			bool firstRegular = true;
			
			foreach (var skater in registration.Skaters) {
				if (skater.Level != null && skater.Level.ToLower () == "basic") {
					total += 12;
				}
				else if (firstRegular) {
					total += 50;
					firstRegular = false;
				}
				else {
					total += 20;
				}
			}
			
			return total;
		}
		
		decimal GetSessionCost(AnnualRegistration registration)
		{
			return registration.Skaters
				.SelectMany(s => s.Sessions)
				.Sum(s => s.TotalCost);	
		}
	}
}