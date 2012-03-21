<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.ViewCalendarItem>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<% if(Model.ID == 0) { %>
		<h2>Create Calendar Item</h2>
	<% } else { %>
		<h2>Edit Calendar Item</h2>
	<% } %>
	<div class="edit-area">
		<% Html.BeginForm("Commit", "CalendarItem"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%//hack: no idea why this wouldn't map right on create new... did manually %>
				<input id="ID" name="ID" type="hidden" value="<%= Model.ID %>" />
				<%= Html.Hidden("RegistrationTermID", Model.RegistrationTermID) %>
			</div>
			<div>
				<label>Days:</label>
				<%= Html.TextBox("Days", Model.Days) %>
			</div>
			<div>
				<label>Saturday Notes:</label>
				<%= Html.TextArea("SaturdayNotes", Model.SaturdayNotes, new {cols="50", rows="10"}) %>
			</div>
			<div>
				<label>Sunday Notes:</label>
				<%= Html.TextArea("SundayNotes", Model.SundayNotes, new {cols="50", rows="10"}) %>
			</div>
			<div>
				<label></label>
				<input type="Submit" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
