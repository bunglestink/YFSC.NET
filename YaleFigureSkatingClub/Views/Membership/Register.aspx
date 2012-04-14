<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="RightBar" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script src="<%= Url.Content("~/Scripts/knockout-2.0.0.js") %>" type="text/javascript"></script> 
    <script src="<%= Url.Content("~/Scripts/knockout.mapping.js") %>" type="text/javascript"></script> 
	<script type="text/javascript" src="<%= Url.Content("~/Scripts/Util/fix-json-date.js") %>"></script>
	<script type="text/javascript" src="<%= Url.Content("~/Scripts/Membership/Register.js") %>"></script>
	<script type="text/javascript">
		
		
		ko.bindingHandlers.button = {
			init: function (element) {
				$(element).button();
			}
		};
		
		$(function () {
			
			// get settings, create and bind view model
			var settings = $('#registration-area').data(),
				viewModel;
		
			
			MODEL.ViewModelFactory.Create(
				settings['current-sessions-url'],
				settings['registration-base-url'],
				settings['get-registration-cost-url'],
				function (vm) {
					viewModel = vm;
	
					// apply bindings, then show first form
	                ko.applyBindings(viewModel);
	                $('#registration-loading').hide();
	                $('#registration-family').slideDown();
	
	                // set datepickers
	                $('.datefield').datepicker();
		
					// register handlers for each nav link
			        $('#registration-navigation ul a, .navlink').each(function () {
			            var $this = $(this);
			            var section = $this.attr('href').substring(1);
			            var properCaseSection = section.substring(0, 1).toUpperCase() + section.substring(1);
			
			            // onclick, hide all sections, then slide down selected
			            $this.click(function () {
			                // hide all 
			                $('#registration-content > fieldset > div').hide();
			                $('#registration-' + section).slideDown();
			                $('#registration-content > fieldset > legend').html(properCaseSection);
			            });
			        });
			
			
			        // hide dialog for later use...
			        $('#session-selector-dialog').dialog({
			            modal: true,
			            autoOpen: false,
			            minWidth: 500,
			            buttons: {
			                'Add Session': function () {
			                    var session = new MODEL.SkaterSession(viewModel.selectedSession(), viewModel.selectedSkater);
			
			                    // delete any previous records of the session before inserting
			                    var deletedSession = viewModel.selectedSkater.Sessions.remove(function (item) { return item.ID == session.ID; });
			                    viewModel.selectedSkater.Sessions.push(session);
			
			                    $(this).dialog('close');
			
			                    // display error message if returns array (has length duck typing)
			                    if (deletedSession.length) {
			                        $('#error-dialog')
			                            .html(viewModel.selectedSkater.FirstName() + ' ' + viewModel.selectedSkater.LastName() + ' already in ' + session.Name)
			                            .dialog('open');
			                    }
			
			                    return true;
			                },
			                Cancel: function () {
			                    $(this).dialog('close');
			                    return false;
			                }
			            }
			        });
			
			        // setup error dialog
			        $('#error-dialog').dialog({
			            modal: true,
			            autoOpen: false,
			            buttons: {
			                Ok: function () {
			                    $(this).dialog('close');
			                    return true;
			                }
			            }
			        });
			
			
			        // setup submit confirmation dialog
			        $('#confirm-submit-dialog').dialog({
			            modal: true,
			            autoOpen: false,
			            minWidth: 500,
			            buttons: {
			                'Ok': function () {
			                    $(this).dialog('close');
			
			                    var result = viewModel.Registration.toJsObject();
		                        $.ajax({
									url: settings['create-registration-url'], 
									data: JSON.stringify(result), 
									type: 'POST',
									contentType: 'application/json',
									success: function (result) {
										if (result === true) {
											UTIL.alert('success!');
											return;
										}
										
										// if not true, model errors: 
										var message = '<p>The following errors were encountered: </p></ul>';
										
										for (var i = 0; i < result.length; i++) {
											message += '<li>' + result[i] + '</li>';
										}
										UTIL.alert(message);
									},
									error: function (jqXhr, statusText) {
										UTIL.alert('An unknown error has occurred: <br /><br />' + statusText);
									}
		                        });
		
		                        return true;
			                },
			
			                'Cancel': function () {
			                    $(this).dialog('close');
			                    return false;
			                }
			            }
			        });
			
		
			        $('#btnSubmitRegistration').click(function () {
			            $('#confirm-submit-dialog').dialog('open');
			        });
			
			
			        // set confirm message on unload
			        window.onbeforeunload = function (e) {
			            e = e || window.event;
			            var message = 'Registration has not been submitted.';
			            // For IE and Firefox prior to version 4
			            if (e) {
			                e.returnValue = message;
			            }
			            // For Safari
			            return message;
			        };
				});
	    });
	</script>		
</asp:Content>



<asp:Content ContentPlaceHolderID="MainContent" runat="server">

	<h2>Registration</h2>
	<div id="registration-area"
		data-current-sessions-url="<%= Url.Action("Current", "SkatingSessionService") %>"
		data-registration-base-url="<%= Url.Action("New", "AnnualRegistrationService") %>"
		data-create-registration-url="<%= Url.Action("Create", "AnnualRegistrationService") %>"
		data-get-registration-cost-url="<%= Url.Action("GetCost", "AnnualRegistrationService") %>">
	    <div id="registration-navigation">
	        <ul>
	            <li><a href="#family">Family</a> </li>
	            <li>&gt; <a href="#skaters">Skaters</a> </li>
	            <li>&gt; <a href="#sessions">Sessions</a> </li>
	            <li>&gt; <a href="#confirmation" data-bind="click: refreshTotalCost">Confirmation</a> </li>
	        </ul>
	    </div>
	
	    <div id="registration-content">
	        <fieldset>
	            <legend>Title</legend>
	
	            <div id="registration-loading">
	                <h3>Loading...</h3>
	            </div>
	
	            <div id="registration-family">
	                <p>Adult or parent of child skater:</p>
	                <div id="registration-family-left">
	                    <div>
	                        <label>First Name:</label>
	                        <input data-bind="value: Registration.FirstName" />
	                    </div>
	
	                    <div>
	                        <label>Middle Name:</label>
	                        <input data-bind="value: Registration.MiddleName" />
	                    </div>
	
	                    <div>
	                        <label>Last Name:</label>
	                        <input data-bind="value: Registration.LastName" />
	                    </div>
	
	                    <div>
	                        <label>Street:</label>
	                        <input data-bind="value: Registration.Street" />
	                    </div>
	
	                    <div>
	                        <label>City:</label>
	                        <input data-bind="value: Registration.City" />
	                    </div>
	
	                    <div>
	                        <label>State:</label>
	                        <input data-bind="value: Registration.State" />
	                    </div>
	
	                    <div>
	                        <label>Zip:</label>
	                        <input data-bind="value: Registration.Zip" />
	                    </div>
	
	                    <div>
	                        <label>Home Phone:</label>
	                        <input data-bind="value: Registration.HomePhone" />
	                    </div>
	
	                    <div>
	                        <label>Work Phone:</label>
	                        <input data-bind="value: Registration.WorkPhone" />
	                    </div>
	
	                    <div>
	                        <label>Email:</label>
	                        <input data-bind="value: Registration.Email" />
	                    </div>
	                </div>
	                <div id="registration-family-right">
	                    <div>
	                        <label>Yale Affiliation:</label>
	                        <input type="checkbox" data-bind="checked: Registration.YaleAffiliation" />
	                    </div>
	                    <div data-bind="visible: Registration.YaleAffiliation">
	                        <div>
	                            <label>Affiliated Person:</label>
	                            <input  data-bind="value: Registration.NameOfAffiliatedPerson" />
	                        </div>
	                        <div>
	                            <label>Yale Affiliation Type:</label>
	                            <input  data-bind="value: Registration.YaleAffiliationType" />
	                        </div>
	                        <div>
	                            <label>Department:</label>
	                            <input  data-bind="value: Registration.Department" />
	                        </div>
	                        <div>
	                            <label>School:</label>
	                            <input  data-bind="value: Registration.School" />
	                        </div>
	                        <div>
	                            <label>Year:</label>
	                            <input  data-bind="value: Registration.Year" />
	                        </div>
	                    </div>
	                </div>
	                <div>
	                    <br />
	                    <a class="navlink" href="#skaters">Next ></a>
	                </div>
	            </div>
	
	
	
	            <div id="registration-skaters">
	                <button data-bind='click: addSkater'>Add Skater</button>
	                <button data-bind='click: copySkater'>Copy Skater From Family</button>
	                <div id="registration-skaters-labels">
	                    <label>First Name</label>
	                    <label>Middle Name</label>
	                    <label>Last Name</label>
	                    <label class="registration-skaters-sex">Sex</label>
	                    <label class="registration-skaters-us-citizen">US Citizen?</label>
	                    <label>DOB</label>
	                    <label>New Registrant?</label>
	                    <label>Level</label>
	                </div>
	                <ul data-bind="template: {name: 'skaterTemplate', foreach: Registration.Skaters}">
	                </ul>
	
	                <script id="skaterTemplate" type="text/html">
	                    <li>
	                        <input data-bind="value: FirstName" />
	                        <input data-bind="value: MiddleName" />
	                        <input data-bind="value: LastName" />
	                        <input data-bind="value: Sex" class="registration-skaters-sex" />
	                        <input data-bind="checked: USCitizen" type="checkbox" class="registration-skaters-us-citizen" />
	                        <input data-bind="value: BirthDate" class="datefield" />
	                        <input data-bind="checked: NewRegistrant" type="checkbox" />
	                        <input data-bind="value: Level" />
	                        <button data-bind="click: remove, button: true">Remove</button>
	                    </li>
	                </script>
	
	                <div>
	                    <br />
	                    <a class="navlink" href="#family">&lt; Back</a> | 
	                    <a class="navlink" href="#sessions">Next &gt;</a>
	                </div>
	            </div>
	
	            <div id="registration-sessions">
	                <h3 data-bind="visible: Registration.Skaters().length == 0">No skaters entered...</h3>
	                <ul data-bind="template: {name: 'skaterSessionTemplate', foreach: Registration.Skaters}"></ul>
	                <script id="skaterSessionTemplate" type="text/html">
	                    <li>
	                        <h3 data-bind="text: fullName"></h3>
	                        <button data-bind="click: $parent.addSession, button: true">Add Session</button>
	                        <p data-bind="visible: Sessions().length == 0">No sessions...</p>
	                        <ul data-bind="template: {name: 'sessionTemplate', foreach: Sessions}"></ul>
	                    </li>
	                </script>
	                <script id="sessionTemplate" type="text/html">
	                    <li>
	                        <span data-bind="text: Name" class="registration-session-name"></span>
	                         &#36<span data-bind="text: TotalCost" class="registration-session-cost"></span>
	                        <button data-bind="click: remove, button: true">Remove</button>
	                    </li>
	                </script>
	
	                <div>
	                    <br />
	                    <a class="navlink" href="#skaters">&lt; Back</a> | 
	                    <a class="navlink" href="#confirmation">Next &gt;</a>
	                </div>
	            </div>
	
	            <div id="registration-confirmation">
	                <div id="submit-area">
	                    <label>Total Cost:</label>
	                    <label data-bind="text: TotalCost"></label>
						<button id="btnSubmitRegistration">Submit Registration</button>
	                </div>
		
					<h3>The <span data-bind="text: Registration.LastName"></span> Family Registration</h3>
	                <p>Please confirm that the below information is correct before proceeding:</p>
		
	                <h4>Family Information</h4>
	                <div id="confirmation-family-left">
	                    <div>
	                        <label>First Name:</label>
	                        <label data-bind="text: Registration.FirstName" />
	                    </div>
	
	                    <div>
	                        <label>Middle Name:</label>
	                        <label data-bind="text: Registration.MiddleName" />
	                    </div>
	
	                    <div>
	                        <label>Last Name:</label>
	                        <label data-bind="text: Registration.LastName" />
	                    </div>
	
	                    <div>
	                        <label>Street:</label>
	                        <label data-bind="text: Registration.Street" />
	                    </div>
	
	                    <div>
	                        <label>City:</label>
	                        <label data-bind="text: Registration.City" />
	                    </div>
	
	                    <div>
	                        <label>State:</label>
	                        <label data-bind="text: Registration.State" />
	                    </div>
	
	                    <div>
	                        <label>Zip:</label>
	                        <label data-bind="text: Registration.Zip" />
	                    </div>
	
	                    <div>
	                        <label>Home Phone:</label>
	                        <label data-bind="text: Registration.HomePhone" />
	                    </div>
	
	                    <div>
	                        <label>Work Phone:</label>
	                        <label data-bind="text: Registration.WorkPhone" />
	                    </div>
	
	                    <div>
	                        <label>Email:</label>
	                        <label data-bind="text: Registration.Email" />
	                    </div>
	                </div>
	                <div id="confirmation-family-right">
	                    <div data-bind="visible: Registration.YaleAffiliation">
	                        <br />
	                        <div>
	                            <label>Yale Affiliation:</label>
	                        </div>
	                        <div>
	                            <label>Affiliated Person:</label>
	                            <label  data-bind="text: Registration.NameOfAffiliatedPerson" />
	                        </div>
	                        <div>
	                            <label>Yale Affiliation Type:</label>
	                            <label  data-bind="text: Registration.YaleAffiliationType" />
	                        </div>
	                        <div>
	                            <label>Department:</label>
	                            <label  data-bind="text: Registration.Department" />
	                        </div>
	                        <div>
	                            <label>School:</label>
	                            <label  data-bind="text: Registration.School" />
	                        </div>
	                        <div>
	                            <label>Year:</label>
	                            <label  data-bind="text: Registration.Year" />
	                        </div>
	                    </div>
	                </div>
	                
	                <h4>Skaters</h4>
	                <ul class="nolist" data-bind="template: {name: 'skaterConfirmTemplate', foreach: Registration.Skaters}">
	                </ul>
	                <script id="skaterConfirmTemplate" type="text/html">
						<li>
	                        <div>
	                            <label>Name:</label>
	                            <span data-bind="text: FirstName" /> <span data-bind="text: MiddleName" /> <span data-bind="text: LastName" /> 
	                        </div>
	                        <div>
	                            <label>Sex:</label>
	                            <label data-bind="text: Sex" />
	                        </div>
	                        <div>
	                            <label>USCitizen</label>
	                            <label data-bind="checked: USCitizen" type="checkbox" />
	                        </div>
	                        <div>
	                            <label>DOB:</label>
	                            <label data-bind="text: BirthDate" class="datefield" />
	                        </div>
	                        <div>
	                            <label>New:</label>
	                            <label data-bind="checked: NewRegistrant" type="checkbox" />
	                        </div>
	                        <div>
	                            <label>Level:</label>
	                            <label data-bind="text: Level" />
	                        </div>
	                        <div>
	                            <label>Sessions:</label>
	                            <ul data-bind="template: {name: 'sessionConfirmTemplate', foreach: Sessions}"></ul>
	                        </div>
	                    </li>
	                </script>
	                <script id="sessionConfirmTemplate" type="text/html">
	                    <li>
	                        <span data-bind="text: Name" class="registration-session-name"></span>
	                    </li>
	                </script>
	                
	
	                <div>
	                    <br />
	                    <a class="navlink" href="#sessions">&lt; Back</a>
	                </div>
	            </div>
	        </fieldset>
	
	    </div>
	</div>
	
	
	<div id="session-selector-dialog" title="Select a Session">
	    <div>
	        <div>
	            <label>Session: </label><select data-bind="options: CurrentSessions, optionsText: 'Name', value: selectedSession"></select>
	        </div>
	        <div>
	            <label>Day of Week: </label><span data-bind="text: selectedSession().DayOfWeek"></span>
	        </div>
	        <div>
	            <label>Start Time: </label><span data-bind="text: selectedSession().StartTime"></span>
	        </div>
	        <div>
	            <label>End Time: </label><span data-bind="text: selectedSession().EndTime"></span>
	        </div>
	        <div>
	            <label>Start Date: </label><span data-bind="text: selectedSession().StartDate"></span>
	        </div>
	        <div>
	            <label>Weeks Duration: </label><span data-bind="text: selectedSession().WeeksDuration"></span>
	        </div>
	        <div>
	            <label>Total Cost: </label>$<span data-bind="text: selectedSession().TotalCost"></span>
	        </div>
	        <p data-bind="text: selectedSession().Description"></p>
	    </div>
	</div>
	
	<div id="error-dialog" title="Message">
	</div>
	
	<div id="confirm-submit-dialog" title="Confirmation">
	    <p>Are you sure you wish to submit this registration?  Once submitted, changes can only be made in person.</p>
	</div>
</asp:Content>