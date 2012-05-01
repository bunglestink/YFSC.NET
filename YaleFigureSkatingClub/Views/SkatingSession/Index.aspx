<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.RegistrationTerm>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2><%= Model.TermName %> Skating Sessions</h2>
	<h5><%= Html.ActionLink("Back to registration terms", "Index", "RegistrationTerm") %></h5>
	<h3><%= Html.ActionLink("Create New Skating Session", "Create", "SkatingSession", new {id = Model.ID}, null) %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Name</th>
			<th>Start Date</th>
			<th>Weeks Duration</th>
			<th>Day Of Week</th>
			<th>Start Time</th>
			<th>End Time</th>
			<th>Cost</th>
			<th>Edit</th>
			<th>Delete</th>
		</tr>	
		<% foreach(var skatingSession in Model.SkatingSessions) { %>
			<tr>
				<td><%= skatingSession.ID %></td>
				<td><%= skatingSession.Name %></td>
				<td><%= skatingSession.StartDate %></td>
				<td><%= skatingSession.WeeksDuration %></td>
				<td><%= skatingSession.DayOfWeek %></td>
				<td><%= skatingSession.StartTime %></td>
				<td><%= skatingSession.EndTime %></td>
				<td><%= skatingSession.TotalCost %></td>
				<td><%= Html.ActionLink("Edit", "Edit", "SkatingSession", new { id = skatingSession.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "SkatingSession", new { id = skatingSession.ID }, null) %></td>
			</tr>	
		<% } %>
	</table>
</asp:Content>
