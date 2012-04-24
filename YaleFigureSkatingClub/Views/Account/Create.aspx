<%@ Page Language="C#" Debug="true" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.User>" %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script type="text/javascript">
				
	$(function () {
		$('form').submit(function () {
			var password = $('#Password').val(),
				passwordConfirm = $('#ConfirmPassword').val();
				
			if (password !== passwordConfirm) {
				UTIL.alert('Password fields must match.');
				return false;
			}
			
			return true;
		});
	});
	
	</script>
</asp:Content>
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
				<%= Html.TextBox("Email") %>
			</div>
			<div>
				<label></label>
				<input type="Submit" value="Create Account" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>