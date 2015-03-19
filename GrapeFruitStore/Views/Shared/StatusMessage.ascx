<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (!String.IsNullOrEmpty(TempData["ErrorMessage"] as string))
   { %>
<div class="error corner-all">
    <%= Html.Encode(TempData["ErrorMessage"]) %>
</div>
<% }
   else if (!String.IsNullOrEmpty(TempData["SuccessMessage"] as string))
   { %>
<div class="success corner-all">
    <%= Html.Encode(TempData["SuccessMessage"]) %>
</div>
<% }
   else
   { %>
<%= Html.Encode(String.Empty) %>
<% } %>
