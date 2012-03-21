<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<YaleFigureSkatingClub.Entities.Announcement>>" MasterPageFile="~/Views/Layout.master"   %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">	
	<h2>News and Announcements</h2>
	<ul>
		<% foreach(var announcment in Model) { %>
			<li><p><label><%= announcment.Title %>:</label> <%= announcment.Body %></p></li>
		<% } %>
	</ul>
	<hr />
	<h2>Welcome New and Returning Members</h2>
	<p>We have had a wonderful fall season and are happy to announce our &ldquo;spring&rdquo; schedule, which will begin on January 7th. A printable <a href="<%= Url.Content("~/Content/files/attach/YFSC1112CalendarSpring.pdf") %>">calendar</a> is attached.</p>
    <p>Elsewhere on this web site you will find a wealth of information about the Club and its <%= Html.ActionLink("programs", "Program", "Home") %>. For questions, please contact us by email at <a href="mailto:yalefsc@gmail.com">yalefsc@gmail.com</a>, leave amessage at 203-432-1233, or contact one of the <%= Html.ActionLink("officers", "ContactInformation", "Home") %>. Please be sure to look over the website before registering, and especially note the following:</p>
	<ul>
		<li>Basic Skills sessions: The format and times are the same as last year.</li>
        <li>Saturday morning advanced sessions: For those who like to get up early, we will continue to have our mixed Moves, Freestyle, and Dance session from 8:15&ndash;9:35. Note that higher level Basic Skills skaters are welcome to subscribe to the last half hour to get more practice or to have private lessons.</li>
        <li>Sunday sessions:
        	<ul>
            	<li>The Sunday evening sessions have been reconfigured into three 1-hour Moves, Freestyle, and Dance sessions (MFD). During MFD1, optional Bridge and Dance group lessons are offered. All skaters in this session are expected to be especially cautious; double jumps and fast-moving, high level skating are not appropriate.</li>
                <li>The Bridge group lesson will be held during the first half hour (5:10&ndash;5:40) of MFD1. Bridge is an introduction to advanced skating: freestyle, moves-in-the-field, dance, and synchronized skating. Those enrolled in Basic Skills and who have passed Level 4 or Adult 2 should seriously consider enrolling for the extra half hour. Those who have completed Basic Skills will want to enroll in both the hour MFD1 session and the half-hour Bridge lesson.</li>
              	<li>The Ice Dance group lesson will be in the second half hour (5:40&ndash;6:10) of Sunday MFD1. As with Bridge, it can be taken as either a half-hour stand-alone session, or added to MFD1.</li>
              	<li>Space permitting, the option of adding an additional half hour is available to those subscribing to MFD2 or MFD3.</li>
            </ul>
        </li>
        <li>Private Lessons: We encourage all skaters to consider taking private lessons. Please email Bobbi Nesheim at bk.titles@comcast.net for advice on finding a coach, or for coach contact information.</li>
    </ul>
	With best wishes for a happy and healthy holiday season,<br /> -- <em>Mary Brett Lee</em>, Membership Chair
</asp:Content>