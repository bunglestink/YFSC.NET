<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.Announcement>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Announcements</h2>
	<h3><%= Html.ActionLink("Create New Announcement", "Create", "Announcement") %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Date</th>
			<th>Title</th>
			<th>Edit</th>
			<th>Delete</th>
		</tr>	
		<% foreach(var announcment in Model) { %>
			<tr>
				<td><%= announcment.ID %></td>
				<td><%= announcment.AnnouncementDate.ToShortDateString() %></td>
				<td><%= announcment.Title %></td>
				<td><%= Html.ActionLink("Edit", "Edit", "Announcement", new { id = announcment.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "Announcement", new { id = announcment.ID }, null) %></td>
			</tr>	
		<% } %>
	</table>
</asp:Content>
