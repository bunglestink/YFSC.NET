<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.RegistrationTerm>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Registration Term Confirmation</h2>
	<p>Are you sure you wish to delete the following registration term?</p>
	<div class="edit-area">
		<% Html.BeginForm("Delete", "RegistrationTerm"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Name:</label>
				<%= Model.TermName %>
			</div>
			<div>
				<label>Start Date:</label>
				<%= Model.StartDate.ToShortDateString() %>
			</div>
			<div>
				<label>End Date:</label>
				<%= Model.EndDate.ToShortDateString() %>
			</div>
			<div>
				<label></label>
				<%= Html.ActionLink("Cancel", "Index", "RegistrationTerm") %> or 
				<input type="Submit" value="Delete" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
