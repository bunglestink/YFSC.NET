<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Invoice>" %>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
<script type="text/javascript">
	$(function () {
		var printInvoice = $('#print-invoice'),
			deletePayment = $('.delete-payment'),
			addPayment = $('#add-payment')
			outstandingBalance = new Number($('.outstanding-balance').data('balance')),
			amountPaid = new Number($('.amount-paid').data('amount'));
		
		function addPaymentTotal(payment) {
			outstandingBalance -= payment;
			amountPaid += payment;
			
			$('.outstanding-balance').html('$' + outstandingBalance.toFixed(2));
			$('.amount-paid').html('$' + amountPaid.toFixed(2));
			
			if (outstandingBalance > 0) {
				$('.outstanding-balance').addClass('not-paid-in-full');
			}
			else { 
				$('.outstanding-balance').removeClass('not-paid-in-full');
			}
		}
		
		function deleteHandler() {
			var $this = $(this),
				paymentAmount = $this.data('amount');
			
			$('#delete-confirm').dialog({
				modal: true,
				autoOpen: true,
				title: 'Confirmation',
				resizable: false,
				buttons: {
					Delete: function () {
						$(this).dialog('close');
						$.ajax({
							url: '<%= Url.Action("DeletePayment", "Invoice") %>/' + $this.data('payment-id'),
							type: 'POST',
							success: function(data) {
								if (data === true) {
									$this.closest('tr').remove();
									addPaymentTotal(-paymentAmount);
									UTIL.alert('Payment successfully deleted.');
									return;
								}
								UTIL.alert('Unable to delete payment.  Please try again later.');
							},
							error: function () {
								UTIL.alert('Unable to delete payment.  Please try again later.');
							}
						});
					},
					Cancel: function () {
						$(this).dialog('close');
					}
				}
			});
			
			return false;
		}
		
		
		
		
		function addHandler() {
			var $this = $(this),
				addDialog = $('#add-dialog');
			
			addDialog.find('input').val(null);
			addDialog.dialog({
				modal: true,
				autoOpen: true,
				title: 'Add Payment',
				width: 400,
				resizable: false,
				buttons: {
					Submit: function () {
						var payment = {
							Amount: parseInt(addDialog.find('#amount').val(), 10),
							Type: addDialog.find('#type').val(),
							Description: addDialog.find('#description').val(),
							Invoice: {ID: $this.data('invoice-id')}
						};
						
						if (!payment.Amount) {
							UTIL.alert('Amount must be a numeric value!');
							return;
						}
						
						$.ajax({
							url: '<%= Url.Action("AddPayment", "Invoice") %>',
							type: 'POST',
							data: JSON.stringify(payment),
							contentType: 'application/json',
							success: function(data) {
								addDialog.dialog('close');
								
								var paymentId = parseInt(data, 10);
								if (paymentId) {
									var paymentTable = $('#payment-table'),
										date = new Date(),
										newRow = $('<tr>')
													.append($('<td>').html((date.getMonth()+1).toString() + '/' + date.getDate().toString() + '/' + date.getFullYear().toString()))
													.append($('<td>').html(payment.Type))
													.append($('<td>').html(payment.Description))
													.append($('<td>').html('$' + payment.Amount.toFixed(2)).addClass('currency'))
													.append($('<td>')
														.append($('<a>')
															.attr('href', '#')
															.data('payment-id', paymentId)
															.data('amount', payment.Amount.toFixed(2))
															.html('Delete').click(deleteHandler)))
													.appendTo(paymentTable.find('tbody'));
									
									addPaymentTotal(payment.Amount);
									UTIL.alert('Payment successfully added.');
									return;
								}
								UTIL.alert('Unable to add payment.  Please try again later.');
							},
							error: function () {
								addDialog.dialog('close');
								UTIL.alert('Unable to add payment.  Please try again later.');
							}
						});
					},
					Cancel: function () {
						$(this).dialog('close');
					}
				}
			});
			
			return false;
		}
		
		
		deletePayment.click(deleteHandler);
		addPayment.click(addHandler);
		printInvoice.click(function () {
			window.print();
			return false;
		});
	});
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Registration Term Invoice</h2>
		<div class="invoice-options">
			<a id="print-invoice" href="#print">Print Invoice</a> | <%= Html.ActionLink("Download as csv file", "Invoice", "Membership", new {id=Model.ID}, null) %>
		</div>
		<hr />
		<br />
		<div>
			<fieldset>
				<legend>Payments</legend>
				<br />
				<a id="add-payment" class="print-hide" data-invoice-id="<%= Model.ID %>" href="#">Add a Payment</a>
				<br />
				<br />
				<table id="payment-table">
					<thead>
						<tr>
							<th>Date Received</th>
							<th>Type</th>
							<th>Notes</th>
							<th>Amount</th>
							<th class="print-hide">Delete</th>
						</tr>
					</thead>
					<tbody>
						<% foreach (var payment in Model.InvoicePayments) { %>
						<tr>
							<td><%= payment.DateReceived.ToShortDateString() %></td>
							<td><%= payment.Type %></td>
							<td><%= payment.Description %></td>
							<td class="currency">$<%= payment.Amount %></td>
							<td class="print-hide"><a href="#" class="delete-payment" data-payment-id="<%= payment.ID %>" data-amount="<%= payment.Amount %>">Delete</a></td>
						</tr>
						<% } %>
					</tbody>
				</table>
			</fieldset>
		</div>
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
				<label>Amount Paid: </label>
				<span data-amount="<%= Model.AmountPaid %>" class="amount-paid">$<%= Model.AmountPaid %></span>
			</div>
			<div>
				<label>Outstanding Balance: </label>
				<span data-balance="<%= Model.OutstandingBalance %>" 
					  class="outstanding-balance <%= Model.PaidInFull ? "" : "not-paid-in-full" %>">$<%= Model.OutstandingBalance %></span>
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
						<% foreach(var item in Model.InvoiceItems) { %>
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
		<div id="delete-confirm" class="dialog">Are you sure you wish to delete this payment?  This action cannot be reversed.</div>
		<div id="add-dialog" class="dialog">
			<div>
				<label>Amount: </label><input type="text" id="amount" />
			</div>			
			<div>
				<label>Type: </label><input type="text" id="type" />
			</div>			
			<div>
				<label>Notes: </label><input type="text" id="description" />
			</div>			
		</div>
</asp:Content>
