<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.Coach>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>YFSC Coaches</h2>
	<p>These professional skaters teach group lessons for the club. Click any link for more information.</p>
	<ul>
		<% foreach(var coach in Model) { %>
			<li><%= Html.ActionLink(coach.Name, "ClubCoaches", "Home", new { id=coach.ID }, null) %></li>
		<% } %>
	</ul>
	<hr />
</asp:Content>