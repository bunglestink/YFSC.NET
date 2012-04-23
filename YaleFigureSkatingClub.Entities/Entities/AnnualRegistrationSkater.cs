using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YaleFigureSkatingClub.Entities
{
    public class AnnualRegistrationSkater
    {
        [Required]
        public virtual int ID { get; set; }

        [Required]
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        
        [Required]
        public virtual string LastName { get; set; }
        
        [Required]
        [StringLength(1)]
        public virtual string Sex { get; set; }

        [Required]
        public virtual bool USCitizen { get; set; }

        [Required]
        public virtual DateTime BirthDate { get; set; }

        [Required]
        public virtual bool NewRegistrant { get; set; }

        [Required]
        public virtual string Level { get; set; }
		
		public virtual AnnualRegistration Registration { get; set; }
        public virtual IList<SkatingSession> Sessions { get; set; }
		
		
		public AnnualRegistrationSkater()
		{
			Sessions = new List<SkatingSession>();
		}
    }
}