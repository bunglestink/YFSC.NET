<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Invoice>" %>Description,Unit Cost,Quantity,Total Cost
<% Response.AddHeader("Content-Disposition", "attachment;filename=invoice.csv");
foreach (var item in Model.InvoiceItems) { %><%= String.Format ("{0},{1},{2},{3}\n", item.Description, item.UnitCost, item.Quantity, item.TotalCost) %><% } %>