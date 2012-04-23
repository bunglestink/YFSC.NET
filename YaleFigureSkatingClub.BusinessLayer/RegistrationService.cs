using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.BusinessLayer
{
	public class RegistrationService : IRegistrationService
	{
		readonly ISession session;
		
		public RegistrationService (ISession session)
		{
			this.session = session;
		}
		
		
		
		
		public AnnualRegistration CreateNewAnnualRegistration()
		{
			return new AnnualRegistration();
		}
		
		
		public void SubmitRegistration (AnnualRegistration registration, User user) 
		{
			registration.User = user;
			
			foreach (var skater in registration.Skaters) {
				var sessions = skater.Sessions;
				skater.Sessions = new List<SkatingSession>();
				skater.Registration = registration;
				
				foreach (var skatingSession in sessions) {
					skater.Sessions.Add (session.Get<SkatingSession>(skatingSession.ID));
				}
			}
			
			session.Save(registration);
			foreach (var skater in registration.Skaters) {
				session.Save (skater);
			}
			GenerateInvoice(registration);
		}
		
		public decimal GetRegistrationCost(AnnualRegistration registration)
		{
			var membershipCost = GetMembershipCost(registration);
			var homeClubCost = GetHomeClubCost(registration);
			var sessionCost = GetSessionCost(registration);
		
			return membershipCost + homeClubCost + sessionCost;
		}
		
		
		void GenerateInvoice(AnnualRegistration registration)
		{
			var invoice = new Invoice();
			invoice.Registration = registration;
			registration.Invoice = invoice;
			var items = invoice.InvoiceItems as List<InvoiceItem>;
			items.AddRange(GetMembershipCostItems(registration));
			items.AddRange(GetHomeClubCostItems(registration));
			items.AddRange(GetSessionCostItems(registration));
			
			session.Save (invoice);
			foreach (var item in invoice.InvoiceItems) {
				session.Save(item);
			}
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
		
		IEnumerable<InvoiceItem> GetMembershipCostItems(AnnualRegistration registration)
		{
			if (registration.Skaters.Count == 0) {
				yield break;
			}
			if (registration.YaleAffiliation) {
				yield return new InvoiceItem {
					Description = "Skater Memebership: Yale affiliated",
					Quantity = registration.Skaters.Count,
					UnitCost = 20,
					Invoice = registration.Invoice
				};
			}
			else {
				yield return new InvoiceItem {
					Description = "Skater Memebership: first non-Yale affiliated",
					Quantity = 1,
					UnitCost = 30,
					Invoice = registration.Invoice
				};
				
				if (registration.Skaters.Count > 1) {
					yield return new InvoiceItem {
						Description = "Skater Memebership: additional non-Yale affiliated",
						Quantity = registration.Skaters.Count - 1,
						UnitCost = 20,
						Invoice = registration.Invoice
					};
				}
			}
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
		
		IEnumerable<InvoiceItem> GetHomeClubCostItems(AnnualRegistration registration)
		{
			var basicSkaterCount = 0;
			var regularSkaterCount = 0;
			
			foreach (var skater in registration.Skaters) {
				if (skater.Level != null && skater.Level.ToLower() == "basic") {
					basicSkaterCount++;
				}
				else {
					regularSkaterCount++;
				}
			}
			
			if (basicSkaterCount > 0) {
				yield return new InvoiceItem {
					Description = "Basic skater / SnoplowSam parent",
					Quantity = basicSkaterCount,
					UnitCost = 12,
					Invoice = registration.Invoice
				};
			}
			if (regularSkaterCount > 0) {
				yield return new InvoiceItem {
					Description = "Regular skater: first in family",
					Quantity = 1,
					UnitCost = 50,
					Invoice = registration.Invoice
				};
			}
			if (regularSkaterCount > 1) {
				yield return new InvoiceItem {
					Description = "Regular skater: additional",
					Quantity = regularSkaterCount - 1,
					UnitCost = 20,
					Invoice = registration.Invoice
				};
			}
		}
		
		
		
		decimal GetSessionCost(AnnualRegistration registration)
		{
			return registration.Skaters
				.SelectMany(s => s.Sessions)
				.Sum(s => s.TotalCost);	
		}
		
		
		IEnumerable<InvoiceItem> GetSessionCostItems(AnnualRegistration registration)
		{
			var sessionCosts = new Dictionary<SkatingSession, int>();
			
			foreach (var skater in registration.Skaters) {
				foreach (var skatingSession in skater.Sessions) {
					if (sessionCosts.ContainsKey(skatingSession)) {
						sessionCosts[skatingSession] = sessionCosts[skatingSession] + 1;
					}
					else {
						sessionCosts.Add (skatingSession, 1);
					}
				}
			}
			
			foreach (var skatingSession in sessionCosts.Keys) {
				yield return new InvoiceItem {
					Description = "Skating session: " + skatingSession.Description,
					Quantity = sessionCosts[skatingSession],
					UnitCost = skatingSession.TotalCost,
					Invoice = registration.Invoice
				};
			}
		}
	}
}