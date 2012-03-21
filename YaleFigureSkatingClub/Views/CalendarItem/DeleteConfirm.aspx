<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.CalendarItem>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Calendar Item Confirmation</h2>
	<p>Are you sure you wish to delete the following calendar item?</p>
	<div class="edit-area">
		<% Html.BeginForm("Delete", "CalendarItem"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Days:</label>
				<%= Model.Days %>
			</div>
			<div>
				<label>Saturday Notes:</label>
				<%= Model.SaturdayNotes %>
			</div>
			<div>
				<label>Sunday Notes:</label>
				<%= Model.SundayNotes %>
			</div>
			<div>
				<label></label>
				<%= Html.ActionLink("Cancel", "Index", "CalendarItem", new {id = Model.RegistrationTerm.ID}, null) %> or 
				<input type="Submit" value="Delete" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
