<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.RegistrationTerm>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2><%= Model.TermName %></h2>
	<table>
		<tr>
			<th>Dates</th>
			<th>Saturday</th>
			<th>Sunday</th>
		</tr>
		<% foreach(var item in Model.CalendarItems) { %>
		<tr>
			<td><%= item.Days %></td>
			<td><%= item.SaturdayNotes %></td>
			<td><%= item.SundayNotes %></td>
		</tr>	
		<% } %>
	</table>
	<hr />
</asp:Content>