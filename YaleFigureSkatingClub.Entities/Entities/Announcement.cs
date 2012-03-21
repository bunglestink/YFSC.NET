using System;
using System.ComponentModel.DataAnnotations;

namespace YaleFigureSkatingClub.Entities
{
	public class Announcement
	{
		[Required]
		public virtual int ID { get; set; }
		public virtual DateTime AnnouncementDate { get; set; }
		public virtual string Title { get; set; }
		public virtual string Body { get; set; }
		
		public Announcement ()
		{
		}
	}
}