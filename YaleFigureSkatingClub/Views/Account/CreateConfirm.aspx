<%@ Page Language="C#" Debug="true" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage<YaleFigureSkatingClub.Entities.User>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Congratulations!</h2>
	<p>Your account has been successfully created!</p>
	<p><%= Html.ActionLink("Enter MemebershipArea", "Index", "Membership")  %></p>
	<hr />
</asp:Content>