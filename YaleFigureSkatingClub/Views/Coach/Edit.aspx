<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Coach>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<% if(Model.ID == 0) { %>
		<h2>Create Coach</h2>
	<% } else { %>
		<h2>Edit Coach</h2>
	<% } %>
	<div class="edit-area">
		<% Html.BeginForm("Commit", "Coach"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Name:</label>
				<%= Html.TextBox("Name", Model.Name) %>
			</div>
			<div>
				<label>Primary Info:</label>
				<%= Html.TextArea("PrimaryInfo", Model.PrimaryInfo, new {cols="50", rows="15"}) %>
			</div>
			<div>
				<label>Secondary Info:</label>
				<%= Html.TextArea("SecondaryInfo", Model.SecondaryInfo, new {cols="50", rows="15"}) %>
			</div>
			<div>
				<label></label>
				<input type="Submit" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
