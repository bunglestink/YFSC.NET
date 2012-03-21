<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h2>Bylaws</h2>
	<div class="formatted-content">
		<div>
			<label><a href="<%= Url.Content("~/Content/files/bylaws/bylaws-amended.pdf") %>">Bylaws</a></label>: as ammended June 22, 2008
		</div>
		<div>
			<label><a href="<%= Url.Content("~/Content/files/bylaws/Exemption_YFSC.pdf") %>">Tax Exemption Certificate</a></label>: May 2007
		</div>
		<div>
			<label>Annual Review Form</label>:
			<ul>
				<li><a href="<%= Url.Content("~/Content/files/bylaws/2010_Form_990-EZ.pdf") %>">"Fiscal Year 2009-10"</a></li>
				<li><a href="<%= Url.Content("~/Content/files/bylaws/2009_Form_990-EZ.pdf") %>">"Fiscal Year 2008-09"</a></li>
				<li><a href="<%= Url.Content("~/Content/files/bylaws/2008_Form_990-EZ.pdf") %>">"Fiscal Year 2007-08"</a></li>
			</ul>
		</div>
	</div>
</asp:Content>
