<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<YaleFigureSkatingClub.Entities.Invoice>>" %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script type="text/javascript">
		$(function () {
			var invoiceFilter = $('#invoice-filter');
			
			invoiceFilter.keyup(function () {
				var filterText = invoiceFilter.val(),
					filterRegex = new RegExp('.*' + filterText + '.*', 'gi');
					
				$('tbody tr').each(function () {
					var $this = $(this),
						id = $this.find('.invoice-id').html(),
						family = $this.find('.invoice-family').html(),
						term = $this.find('.invoice-term').html();
						
					if (filterRegex.test(id) || filterRegex.test(family) || filterRegex.test(term)) {
						$this.show();
					}
					else {
						$this.hide();
					}
				});
			});
		});
	</script>
</asp:Content>		
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Invoices</h2>
	Filter: <input id="invoice-filter" />
	<hr />
	<table>
		<thead>
			<tr>
				<th>ID</th>
				<th>Family Name</th>
				<th>Registration Term</th>
				<th>Amount</th>
				<th>Edit</th>
			</tr>	
		</thead>
		<tbody>
			<% foreach(var invoice in Model) { %>
				<tr>
					<td class="invoice-id"><%= invoice.ID %></td>
					<td class="invoice-family"><%= invoice.Registration.FirstName %> <%= invoice.Registration.LastName %></td>
					<td class="invoice-term"><%= invoice.Registration.RegistrationTerm.TermName %></td>
					<td>$<%= invoice.OutstandingBalance %></td>
					<td><%= Html.ActionLink("Edit", "View", "Invoice", new { id = invoice.ID }, null) %></td>
				</tr>
			<% } %>
		</tbody>
	</table>
</asp:Content>