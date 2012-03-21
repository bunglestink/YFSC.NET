using System;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.Models
{
	public class ViewCalendarItem : CalendarItem
	{
		public virtual int? RegistrationTermID { get; set; }
		
		public ViewCalendarItem () : base()
		{
		}
		
		public ViewCalendarItem(CalendarItem calendarItem)
		{
			this.ID = calendarItem.ID;
			this.Days = calendarItem.Days;
			this.SaturdayNotes = calendarItem.SaturdayNotes;
			this.SundayNotes = calendarItem.SundayNotes;
			this.RegistrationTerm = calendarItem.RegistrationTerm;
			
			if (this.RegistrationTerm != null)
			{
				this.RegistrationTermID = this.RegistrationTerm.ID;
			}
		}
		
		
		/// <summary>
		/// Converts this view item into a standard calendar item
		/// </summary>
		public CalendarItem ToCalendarItem()
		{
			return new CalendarItem()
			{
				ID = this.ID,
				Days = this.Days,
				SaturdayNotes = this.SaturdayNotes,
				SundayNotes = this.SundayNotes,
				RegistrationTerm = this.RegistrationTerm	
			};
		}
	}
}