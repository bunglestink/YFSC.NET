;
MODEL = (function () {
	
	var ViewModelFactory = {
		
		Create: function (currentSessionsUrl, registrationBaseUrl, callback) {
			var viewModel = {
		        // determines wether the data is finished loading
		        loadingComplete: function () {
		            return this.Registration && this.CurrentSessions;
		        },
		
		        // finish creation if loading complete, then call callback
		        dataBind: function () {
		            if (this.loadingComplete()) {
		                // bind selected session
		                this.selectedSession = ko.observable(this.CurrentSessions[0]);
		                
		                // build view methods
		                this.addSkater = function () {
					        this.Registration.Skaters.push(new Skater(this.Registration));
					        $('.datefield').datepicker();
					    };
					    this.copySkater = function () {
					        var skater = new Skater(this.Registration);
					        skater.FirstName(this.Registration.FirstName());
					        skater.LastName(this.Registration.LastName());
					        skater.MiddleName(this.Registration.MiddleName());
					        this.Registration.Skaters.push(skater);
					        $('.datefield').datepicker();
					    }
		
		                callback(viewModel);
		            }
		        }
		    };
		    
		    // get the viewModel, load into view
		    $.getJSON(currentSessionsUrl, function (data) {
		        // assign viewModel from server, then try to dataBind
		        viewModel.CurrentSessions = ko.observableArray(data);
		        viewModel.dataBind();
		    });
		
		    $.getJSON(registrationBaseUrl, function (data) {
		        // assign viewModel from server, then try to dataBind
		        viewModel.Registration = new Registration(data);
		        viewModel.dataBind();
		    });
		}
	};
	
	
	
	/** Registration **/
	function Registration(data) {
		var self = $.extend(this, ko.mapping.fromJS(data));
        self.skaterCount = ko.dependentObservable(function () { return self.Skaters().length; });
        return self;
	}
	
	Registration.prototype.toJSON = function () {
        var result = {};
        result.ID = this.ID();
        result.FirstName = this.FirstName();
        result.MiddleName = this.MiddleName();
        result.LastName = this.LastName();
        result.Street = this.Street();
        result.City = this.City();
        result.State = this.State();
        result.Zip = this.Zip();
        result.HomePhone = this.HomePhone();
        result.WorkPhone = this.WorkPhone();
        result.Email = this.Email();
        result.YaleAffiliation = this.YaleAffiliation();
        result.NameOfAffiliatedPerson = this.NameOfAffiliatedPerson();
        result.YaleAffiliationType = this.YaleAffiliationType();
        result.Department = this.Department();
        result.School = this.School();
        result.Year = this.Year();
        result.Skaters = [];
        for (var skaterKey in this.Skaters()) {
            var skater = this.Skaters()[skaterKey];
            var jsonSkater = {};
            jsonSkater.FirstName = skater.FirstName();
            jsonSkater.LastName = skater.LastName();
            result.Skaters.push(jsonSkater);
        } 
        return result;
    };
	/** End Registration **/
	
	
	/** Skater **/
	function Skater(registration) {
	    this.registration = registration;
	    
	    this.ID = ko.observable(0);
	    this.FirstName = ko.observable('');
	    this.MiddleName = ko.observable('');
	    this.LastName = ko.observable('');
	    this.Sex = ko.observable('');
	    this.USCitizen = ko.observable(true);
	    this.BirthDate = ko.observable('');
	    this.NewRegistrant = ko.observable(true);
	    this.Level = ko.observable('');
	    this.Sessions = ko.observableArray([]);
	    
	    this.fullName = ko.dependentObservable(function () {
	        return this.FirstName() + ' ' + this.LastName();
	    }, this);
	}
	
	Skater.prototype.remove = function () {
	    this.registration.Skaters.remove(this);
	};
	Skater.prototype.addSession = function () {
	    viewModel.selectedSkater = this;
	    $('#session-selector-dialog').dialog('open');
	};
	/** End Skater **/
	
	
	/** SkaterSession - represents a skater session **/
	function SkaterSession(session, skater) {
	    this.ID = session.ID;
	    this.Name = session.Name;
	    this.StartDate = session.StartDate;
	    this.DayOfWeek = session.DayOfWeek;
	    this.StartTime = session.StartTime;
	    this.EndTime = session.EndTime;
	    this.TotalCost = session.TotalCost;
	    this.WeeksDuration = session.WeeksDuration;
	    this.Description = session.Description;
	    
	    this.Skater = skater;
	};
	SkaterSession.prototype.remove = function() {
	    this.Skater.Sessions.remove(this);
	};
	/** End SkaterSession **/
	
	
	
	/* public interface */
	return {
		Skater: Skater,
		SkaterSession: SkaterSession,
		ViewModelFactory: ViewModelFactory
	};
}());