<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.User>" %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script type="text/javascript">
	$(function () {
		$('#backup-action').click(function () {
			
			$('#backup-confirm').dialog({
				autoOpen: true,
				modal: true,
				resizable: false,
				title: 'Confirmation',
				buttons: {
					'Yes, perform backup now': function () {
						window.location = $('#backup-action').attr('href');
						$(this).dialog('close');
					},
					'Cancel': function() {
						$(this).dialog('close');
					}
				},
				close: function () {
					$(this).dialog('destroy');
				}
			});
			
			return false;
		});
	});
	</script>
</asp:Content>	
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Membership</h2>
	<div class="formatted-content">
		<div>
			<%= Html.ActionLink("Signup for current Session now!", "Register", "Membership") %>
		</div>
	</div>
	<div>
		<div>
			<fieldset>
				<legend>Registration History</legend>
				<br />
				<% if (!Model.Registrations.Any()) { %>
					<p>You have no past registrations.</p>
				<% } else { %>
					<table>
						<thead>
							<tr>
								<th>Registration Term</th>
								<th>Registration Date</th>
								<th>Number of Skaters</th>
								<th>Total Cost</th>
								<th>Amount Paid</th>
								<th>View Invoice</th>
								<th>Download</th>
							</tr>
						</thead>
						<tbody>
							<% foreach (var registration in Model.Registrations) { %>
								<tr>
									<td><%= registration.RegistrationTerm.TermName %></td>
									<td><%= registration.Invoice.InvoiceDate == null ? "" : registration.Invoice.InvoiceDate.ToShortDateString() %></td>
									<td><%= registration.Skaters.Count %></td>
									<td class="currency">$<%= registration.Invoice.TotalCost %></td>
									<td class="currency">$<%= registration.Invoice.AmountPaid %></td>
									<td><%= Html.ActionLink("View", "Invoice", "Membership", new { id = registration.Invoice.ID }, null) %></td>
									<td><%= Html.ActionLink("csv file", "Invoice", "Membership", new { id = registration.Invoice.ID, format = "csv" }, null) %></td>
								</tr>
							<% } %>
						</tbody>
					</table>
				<% } %>
			</fieldset>
		</div>
		<div>
			<% if (User.IsInRole("Admin")) { %>
				<fieldset>
					<legend>Admin</legend>
					<ul>
						<li><%= Html.ActionLink("Announcements", "Index", "Announcement") %></li>
						<li><%= Html.ActionLink("Coaches", "Index", "Coach") %></li>
						<li><%= Html.ActionLink("Invoices", "Index", "Invoice") %></li>
						<li><%= Html.ActionLink("Registration Terms", "Index", "RegistrationTerm") %></li>
					</ul>
				</fieldset>
				<fieldset>
					<legend>Maintenance</legend>
					<ul>
						<li><%= Html.ActionLink("Backup System", "Backup", "AdminServices", null, new { id="backup-action" }) %></li>
					</ul>
				</fieldset>
				<div id="backup-confirm" class="dialog">This action will create a backup of the entire system.  This can be used for archival purposes, and can be restored by an admin.  Are you sure you wish to backup the system?</div>
			<% } %>
		</div>
	</div>
</asp:Content>
