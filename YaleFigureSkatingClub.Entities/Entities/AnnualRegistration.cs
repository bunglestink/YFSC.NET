using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YaleFigureSkatingClub.Entities
{
    public class AnnualRegistration
    {
        [Required]
        public virtual int ID { get; set; }

        [Required]
        public virtual string FirstName { get; set; }
        
        public virtual string MiddleName {get;set;}
        
        [Required]
        public virtual string LastName { get; set; }
        
        [Required]
        public virtual string Street { get; set; }

        [Required]
        public virtual string City { get; set; }

        [Required]
        [StringLength(2)]
        public virtual string State { get; set; }
        
        [Required]
        [StringLength(5)]
        public virtual string Zip { get; set; }

        [Required]
        public virtual string HomePhone { get; set; }
        public virtual string WorkPhone { get; set; }
        public virtual string Email { get; set; }

        [Required]
        public virtual bool YaleAffiliation { get; set; }
        public virtual string NameOfAffiliatedPerson { get; set; }
        public virtual string YaleAffiliationType { get; set; }
        public virtual string Department { get; set; }
        public virtual string School { get; set; }
        public virtual string Year { get; set; }

		
		public virtual RegistrationTerm RegistrationTerm { get; set; }
        public virtual IList<AnnualRegistrationSkater> Skaters { get; set; }

        /// <summary>
        /// Creates a new AnnualRegistration
        /// </summary>
        public AnnualRegistration()
        {
            Skaters = new List<AnnualRegistrationSkater>();
        }
    }
}