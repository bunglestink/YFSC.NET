<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.RegistrationTerm>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Registration Terms</h2>
	<h3><%= Html.ActionLink("Create New Registration Term", "Create", "RegistrationTerm") %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Name</th>
			<th>StartDate</th>
			<th>EndDate</th>
			<th>Calendar Items</th>
			<th>Edit</th>
			<th>Delete</th>
		</tr>	
		<% foreach(var term in Model) { %>
			<tr>
				<td><%= term.ID %></td>
				<td><%= term.TermName %></td>
				<td><%= term.StartDate.ToShortDateString() %></td>
				<td><%= term.EndDate.ToShortDateString() %></td>
				<td>
					<%= term.CalendarItems.Count %> - <%= Html.ActionLink("View", "Index", "CalendarItem", new { id = term.ID }, null) %>
				</td>
				<td><%= Html.ActionLink("Edit", "Edit", "RegistrationTerm", new { id = term.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "RegistrationTerm", new { id = term.ID }, null) %></td>
			</tr>	
		<% } %>
	</table>
</asp:Content>
