<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Announcement>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Announcement Confirmation</h2>
	<p>Are you sure you wish to delete the following announcement?</p>
	<div class="edit-area">
		<% Html.BeginForm("Delete", "Announcement"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Date:</label>
				<%= Model.AnnouncementDate.ToShortDateString() %>
			</div>
			<div>
				<label>Title:</label>
				<%= Model.Title %>
			</div>
			<div>
				<label>Body:</label>
				<%= Model.Body %>
			</div>
			<div>
				<label></label>
				<%= Html.ActionLink("Cancel", "Index", "Announcement") %> or 
				<input type="Submit" value="Delete" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
