<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Models.ViewSkatingSession>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<% if(Model.ID == 0) { %>
		<h2>Create Skating Session</h2>
	<% } else { %>
		<h2>Edit Skating Session</h2>
	<% } %>
	<div class="edit-area">
		<% Html.BeginForm("Commit", "SkatingSession"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%//hack: no idea why this wouldn't map right on create new... did manually %>
				<input id="ID" name="ID" type="hidden" value="<%= Model.ID %>" />
				<%= Html.Hidden("RegistrationTermID", Model.RegistrationTermID) %>
			</div>
			<div>
				<label>Name:</label>
				<%= Html.TextBox("Name", Model.Name) %>
			</div>
			<div>
				<label>Start Date:</label>
				<%= Html.TextBox("Start Date", Model.StartDate.ToShortDateString(), new { @class="date" }) %>
			</div>
			<div>
				<label>Weeks Duration:</label>
				<%= Html.TextBox("WeeksDuration", Model.WeeksDuration) %>
			</div>
			<div>
				<label>Day Of Week:</label>
				<%= Html.TextBox("DayOfWeek", Model.DayOfWeek) %>
			</div>
			<div>
				<label>Start Time:</label>
				<%= Html.TextBox("StartTime", Model.StartTime) %>
			</div>
			<div>
				<label>End Time:</label>
				<%= Html.TextBox("EndTime", Model.EndTime) %>
			</div>
			<div>
				<label>Cost:</label>
				<%= Html.TextBox("TotalCost", Model.TotalCost), new {@class="currency"} %>
			</div>
			
			<div>
				<label>Description:</label>
				<%= Html.TextArea("Description", Model.Description, new {cols="50", rows="5"}) %>
			</div>
			<div>
				<label></label>
				<input type="Submit" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
