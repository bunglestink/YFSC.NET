<%@ Page Language="C#" MasterPageFile="~/Views/Layout.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div class="formatted-content">
		<div>
			<h2>Join Online Now!</h2>
			<p>The easiest way to join YFSC is by online electronic registraion.  If you already have an account, sign in and register!  If not, please take a minute and create your account today:</p>
			<div class="join-now">
				<% if (Request.IsAuthenticated) { %>
					<p><%= Html.ActionLink("Join Online in Member Area", "Index", "Membership") %></p> 
				<% } else { %>
					<p><%= Html.ActionLink("Login", "LogOn", "Account") %></p> 
					<p>or</p>
					<p><%= Html.ActionLink("Create an Account", "Create", "Account") %></p>
				<% } %>
			</div>
		</div>
		<hr />
		<div>
			<h2>Join By Mail</h2>
			<div>
				Click the links for the forms you need.<br />
				Print the forms, fill them out, and mail them with a check to:<br />
				<div class="padded-address">
					Mary D'Agostino,<br />
					19 Douglas Drive,<br />
					Hamden, CT 06518<br />
				</div>
			</div>
			<ol>
				<li>
					<a href="<%= Url.Content("~/Content/files/attach/1112_YFSC_Spring_Renewal.pdf") %>">Spring Registration Form for Current Members</a><br />
					Use this form to subscribe to Spring term skating sessions.<br />
				</li>
				<li>
					<a href="<%= Url.Content("~/Content/files/attach/1112_YFSC_Mbrship_FormSpring.pdf") %>">Combined Membership and Registration Form for New Members</a><br />
					Use this form if you were NOT a YFSC member in the fall, to join the YFSC and/or USFSA and to register for spring term sessions.
				</li>
				<li>
					<a href="<%= Url.Content("~/Content/files/attach/Emergency_Form.pdf") %>">Emergency Information</a><br />
					Required for new skating members.  This information will be kept in the rink.<br />
				</li>
				<li>
					Test Application Forms<br />
					No test session has been scheduled yet.<br />
				</li>
			</ol>
		</div>
	</div>
</asp:Content>
