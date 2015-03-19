<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IQueryable<GrapeFruitStore.Models.Department>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - ������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ������</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <ul>
        <% foreach (var department in Model)
           { %>
        <li>
            <%= Html.ActionLink(department.Title, "Department", new { id = department.DepartmentID })%>
        </li>
        <% } %>
    </ul>
    <% if (Context.User.IsInRole(Constants.AdministratorsRoleName))
       { %>
    <p>
        <%= Html.ActionLink("����� �����", "CreateDepartment", null, new { @class = "button new" })%>
    </p>
    <% } %>
</asp:Content>
