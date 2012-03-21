<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.RegistrationTerm>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<% if(Model.ID == 0) { %>
		<h2>Create Registration Term</h2>
	<% } else { %>
		<h2>Edit Registration Term</h2>
	<% } %>
	<div class="edit-area">
		<% Html.BeginForm("Commit", "RegistrationTerm"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Name:</label>
				<%= Html.TextBox("TermName", Model.TermName) %>
			</div>
			<div>
				<label>Start Date:</label>
				<%= Html.TextBox("StartDate", Model.StartDate.ToShortDateString(), new { @class="date" }) %>
			</div>
			<div>
				<label>End Date:</label>
				<%= Html.TextBox("EndDate", Model.EndDate.ToShortDateString(), new { @class="date" }) %>
			</div>
			<div>
				<label></label>
				<input type="Submit" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
