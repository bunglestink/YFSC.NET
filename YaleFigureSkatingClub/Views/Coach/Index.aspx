<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.Coach>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Coaches</h2>
	<h3><%= Html.ActionLink("Add a Coach", "Create", "Coach") %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Name</th>
			<th>Edit</th>
			<th>Delete</th>
		</tr>	
		<% foreach(var coach in Model) { %>
			<tr>
				<td><%= coach.ID %></td>
				<td><%= coach.Name %></td>
				<td><%= Html.ActionLink("Edit", "Edit", "Coach", new { id = coach.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "Coach", new { id = coach.ID }, null) %></td>
			</tr>
		<% } %>
	</table>
</asp:Content>
