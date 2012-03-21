<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Announcement>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script src="<%= Url.Content("~/Scripts/edit_area/edit_area_full.js") %>" type="text/javascript"></script>
	<script type="text/javascript">
		editAreaLoader.init({
			id: 'Body',
			start_highlight: true,
			allow_resize: 'both',
			allow_toggle: false,
			word_wrap: false,
			language: 'en',
			syntax: 'html'
		});
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<% if(Model.ID == 0) { %>
		<h2>Create Announcement</h2>
	<% } else { %>
		<h2>Edit Announcement</h2>
	<% } %>
	<div class="edit-area">
		<% Html.BeginForm("Commit", "Announcement"); %>
		<fieldset>
			<div>
				<label>ID:</label>
				<%= Model.ID %>
				<%= Html.Hidden("ID", Model.ID) %>
			</div>
			<div>
				<label>Date:</label>
				<%= Html.TextBox("AnnouncementDate", Model.AnnouncementDate.ToShortDateString(), new { @class="date" }) %>
			</div>
			<div>
				<label>Title:</label>
				<%= Html.TextBox("Title", Model.Title) %>
			</div>
			<div>
				<label>Body:</label>
				<%= Html.TextArea("Body", Model.Body, new {cols="50", rows="20"}) %>
			</div>
			<div>
				<label></label>
				<input type="Submit" />
			</div>
		</fieldset>
		<% Html.EndForm(); %>
	</div>
</asp:Content>
