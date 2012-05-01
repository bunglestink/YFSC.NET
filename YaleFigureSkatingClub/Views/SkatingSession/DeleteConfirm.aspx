<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Models.ViewSkatingSession>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Calendar Item Confirmation</h2>
	<p>Are you sure you wish to delete the following calendar item?</p>
	<div class="edit-area">
		<% Html.BeginForm("Delete", "SkatingSession"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Name:</label>
				<%= Model.Name %>
			</div>
			<div>
				<label>Start Date:</label>
				<%= Model.StartDate %>
			</div>
			<div>
				<label>Weeks Duration:</label>
				<%= Model.WeeksDuration %>
			</div>
			<div>
				<label>Day Of Week:</label>
				<%= Model.DayOfWeek %>
			</div>
			<div>
				<label>Start Time:</label>
				<%= Model.StartTime %>
			</div>
			<div>
				<label>End Time:</label>
				<%= Model.EndTime %>
			</div>
			<div>
				<label>Cost:</label>
				<%= Model.TotalCost %>
			</div>
			<div>
				<label>Description:</label>
				<%= Model.Description %>
			</div>
			<div>
				<label></label>
				<%= Html.ActionLink("Cancel", "Index", "SkatingSession", new {id = Model.RegistrationTermID}, null) %> or 
				<input type="Submit" value="Delete" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
