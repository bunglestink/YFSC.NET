<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Membership</h2>
	<div class="formatted-content">
		<div>
			<%= Html.ActionLink("Signup for current Session now!", "Register", "Membership") %>
		</div>
	</div>
	<div>
		<% if (User.IsInRole("Admin")) { %>
			<fieldset>
				<legend>Admin</legend>
				<ul>
					<li><%= Html.ActionLink("Announcements", "Index", "Announcement") %></li>
					<li><%= Html.ActionLink("Coaches", "Index", "Coach") %></li>
					<li><%= Html.ActionLink("Registration Terms", "Index", "RegistrationTerm") %></li>
				</ul>
			</fieldset>
		<% } %>
	</div>
</asp:Content>
