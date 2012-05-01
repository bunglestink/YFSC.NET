using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace YaleFigureSkatingClub.Entities
{
    public class SkatingSession
    {
		[Required]
        public virtual int ID { get; set; }
		
        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual int WeeksDuration { get; set; }
        public virtual string DayOfWeek { get; set; }
        public virtual string StartTime { get; set; }
        public virtual string EndTime { get; set; }
        public virtual decimal TotalCost { get; set; }
        public virtual string Description { get; set; }
		
		public SkatingSession()
		{
		}
    }
}
