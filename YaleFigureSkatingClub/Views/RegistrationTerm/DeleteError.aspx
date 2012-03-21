<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>Delete Error</h2>
	<p>This Registration Term cannot be deleted because it has calendar items.</p>
	<div>
		<%= Html.ActionLink("Back", "Index", "RegistrationTerm") %>
	</div>
	<hr />
</asp:Content>
