<%@ Page Language="C#" Debug="true" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.User>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Create an Account</h2>
	<p>Complete the below to create a new account.</p>
	<div class="edit-area">
		<% Html.BeginForm("Create", "Account"); %>
		<fieldset>
			<div>
				<%= Html.ValidationSummary() %>
			</div>
			<div>
				<label>Username</label>
				<%= Html.TextBox("Username", Model.Username) %>
			</div>
			<div>
				<label>Password</label>
				<%= Html.Password("Password") %>
			</div>
			<div>
				<label>Password Confirm</label>
				<%= Html.Password("ConfirmPassword") %>
			</div>
			<div>
				<label>Email Address</label>
				<%= Html.Password("Email") %>
			</div>
			<div>
				<label></label>
				<input type="Submit" value="Create Account" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>