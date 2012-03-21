<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Coach>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Coach Confirmation</h2>
	<p>Are you sure you wish to delete the following coach?</p>
	<hr />
	<div class="edit-area">
		<% Html.BeginForm("Delete", "Coach"); %>
		<div>
			<label>ID:</label>
			<%= Model.ID %>
			<%= Html.Hidden("ID", Model.ID) %>
		</div>
		<div>
			<label>Name:</label>
			<%= Model.Name %>
		</div>
		<div>
			<label>Primary Info:</label>
			<%= Model.PrimaryInfo %>
		</div>
		<div>
			<label>SecondaryInfo:</label>
			<%= Model.SecondaryInfo %>
		</div>
		<div>
			<label></label>
			<%= Html.ActionLink("Cancel", "Index", "Coach") %> or 
			<input type="Submit" value="Delete" />
		</div>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
