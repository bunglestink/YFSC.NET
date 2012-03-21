<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.Coach>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2><%= Model.Name %></h2>
	<hr />
	<div id="coach-primary-info">
		<%= Model.PrimaryInfo %>
	</div>
	<div id="coach-secondary-info">
		<%= Model.SecondaryInfo %>
	</div>
</asp:Content>