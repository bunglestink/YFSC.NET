<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.RegistrationTerm>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script type="text/javascript">
	$(function () {
		$('.set-current').click(function () {
			var $this = $(this),
				setCurrentUrl = $this.data('set-current-url');
	
			$('#set-current-confirm').dialog({
				autoOpen: true,
				modal: true,
				title: 'Confirmation',
				buttons: {
					'Yes, change current': function () {
						$.ajax({
							type: 'post',
							url: setCurrentUrl,
							success: function (data) {
								if (data !== true) {
									UTIL.alert('An error occurred.  Please try again later.');
									return;
								}
	
								$('tr').removeClass('current-term-row');
								$this.closest('tr').addClass('current-term-row');
								UTIL.alert('You have successfully changed to current registration!');
							},
							error: function () {
								UTIL.alert('An error occurred.  Please try again later.');
							}
						});
						$(this).dialog('close');
					},
	
					'Cancel': function () {
						$(this).dialog('close');
					}
				},
				close: function () {
					$(this).dialog('destroy');
				}
			});
		});
	});
	</script>
</asp:Content>	
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Registration Terms</h2>
	<h3><%= Html.ActionLink("Create New Registration Term", "Create", "RegistrationTerm") %></h3>
	<table>
		<tr>
			<th>ID</th>
			<th>Name</th>
			<th>StartDate</th>
			<th>EndDate</th>
			<th>Calendar Items</th>
			<th>Skating Sessions</th>
			<th>Edit</th>
			<th>Delete</th>
			<th>Current</th>
		</tr>	
		<% foreach(var term in Model) { %>
			<tr class="<%: term.Current ? "current-term-row" : String.Empty %>">
				<td><%= term.ID %></td>
				<td><%= term.TermName %></td>
				<td><%= term.StartDate.ToShortDateString() %></td>
				<td><%= term.EndDate.ToShortDateString() %></td>
				<td>
					<%= term.CalendarItems.Count %> - <%= Html.ActionLink("View", "Index", "CalendarItem", new { id = term.ID }, null) %>
				</td>
				<td>
					<%= term.SkatingSessions.Count %> - <%= Html.ActionLink("View", "Index", "SkatingSession", new { id = term.ID }, null) %>
				</td>
				<td><%= Html.ActionLink("Edit", "Edit", "RegistrationTerm", new { id = term.ID }, null) %></td>
				<td><%= Html.ActionLink("Delete", "DeleteConfirm", "RegistrationTerm", new { id = term.ID }, null) %></td>
				<td>
					<span class="current-term"><strong>Current Term</strong></span>
					<a href="#" class="set-current" 
						data-set-current-url="<%: Url.Action("SetCurrent", "RegistrationTerm", new { id = term.ID }) %>">Set as Current</a>
				</td>
			</tr>	
		<% } %>
	</table>
	<div id="set-current-confirm" class="dialog">Are you sure you wish to change the current registration term?  This has system wide changes.</div>
</asp:Content>
