<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.User>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Log In</h2>
	<div class="edit-area">
		<% Html.BeginForm("LogOn", "Account"); %>
		<%= Html.Hidden("returnUrl", Request["ReturnUrl"]) %>
		<fieldset>
			<div>
				<label>Username</label>
				<%= Html.TextBox("Username", Model.Username) %>
			</div>
			<div>
				<label>Password</label>
				<%= Html.Password("Password") %>
			</div>
			<div>
				<label></label>
				<input type="Submit" value="Log In" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
		<p>Don't have an account? <%= Html.ActionLink("Create one in seconds!", "Create", "Account") %></p>
	</div>
</asp:Content>