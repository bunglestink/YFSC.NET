﻿<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Invoice>" %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<script type="text/javascript">
	$(function () {
		$('#print-invoice').click(function () {
			window.print();
			return false;
		});
	});
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Registration Term Invoice</h2>
		<div class="invoice-options">
			<a id="print-invoice" href="#print">Print Invoice</a> | <%= Html.ActionLink("Download as csv file", "Invoice", "Membership", new {id = Model.ID, format="csv"}, null) %>
		</div>
		<hr />
		<br />
		<div class="formatted-content invoice-header">
			<div>
				<label>Invoice ID: </label><span><%= Model.ID %></span>
			</div>
			<div>
				<label>Invoice Date: </label><span><%= Model.InvoiceDate.ToShortDateString() %></span>
			</div>
			<div>
				<label>Total Cost</label><span>$<%= Model.TotalCost %></span>
			</div>
			<div>
				<label>Amount Paid: </label><span>$<%= Model.AmountPaid %></span>
			</div>
			<div>
				<label>Outstanding Balance: </label><span class="<%= Model.PaidInFull ? "" : "not-paid-in-full" %>">$<%= Model.OutstandingBalance %></span>
			</div>
		</div>
		<div>
			<fieldset>
				<legend>Invoice Items</legend>
				<br />
				<table>
					<thead>
						<tr>
							<th>Description</th>
							<th>Unit Cost</th>
							<th>Quantity</th>
							<th>Total Cost</th>
						</tr>
					</thead>
					<tbody>
						<% foreach (var item in Model.InvoiceItems) { %>
						<tr>
							<td><%= item.Description %></td>
							<td class="currency">$<%= item.UnitCost %></td>
							<td><%= item.Quantity %></td>
							<td class="currency">$<%= item.TotalCost %></td>
						</tr>
						<% } %>
					</tbody>
				</table>
			</fieldset>
		</div>
		<div>
			<fieldset>
				<legend>Payments</legend>
				<br />
				<table>
					<thead>
						<tr>
							<th>Date Received</th>
							<th>Type</th>
							<th>Notes</th>
							<th>Amount</th>
						</tr>
					</thead>
					<tbody>
						<% foreach (var payment in Model.InvoicePayments) { %>
						<tr>
							<td><%= payment.DateReceived.ToShortDateString() %></td>
							<td><%= payment.Type %></td>
							<td><%= payment.Description %></td>
							<td class="currency">$<%= payment.Amount %></td>
						</tr>
						<% } %>
					</tbody>
				</table>
			</fieldset>
		</div>
</asp:Content>