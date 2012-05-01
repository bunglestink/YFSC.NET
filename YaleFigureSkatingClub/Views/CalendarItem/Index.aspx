<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.RegistrationTerm>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2><%= Model.TermName %> Calendar Items</h2>
	<h5><%= Html.ActionLink("Back to registration terms", "Index", "RegistrationTerm") %></h5>
	<h3><%= Html.ActionLink("Create New Calendar Item", "Create", "CalendarItem", new {id = Model.ID}, null) %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Days</th>
			<th>Saturday Notes</th>
			<th>Sunday Notes</th>
			<th>Edit</th>
			<th>Delete</th>
		</tr>	
		<% foreach(var item in Model.CalendarItems) { %>
			<tr>
				<td><%= item.ID %></td>
				<td><%= item.Days %></td>
				<td><%= item.SaturdayNotes %></td>
				<td><%= item.SundayNotes %></td>
				<td><%= Html.ActionLink("Edit", "Edit", "CalendarItem", new { id = item.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "CalendarItem", new { id = item.ID }, null) %></td>
			</tr>	
		<% } %>
	</table>
</asp:Content>
