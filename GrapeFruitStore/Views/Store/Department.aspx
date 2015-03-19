<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Controllers.StoreViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(String.Format("GrapeFruit Store - {0}", Model.Department.Title)) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(Model.Department.Title) %>
    </h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <p class="boxed corner-all">
        <%= Html.Encode(Model.Department.Description) %>
    </p>
    <% if (Model.ProductsList.ToList().Count == 0)
       { %>
    <p>
        <i>В этом отделе еще нет товаров.</i>
    </p>
    <% }
       else
       { %>
    <ul>
        <% foreach (var product in Model.ProductsList)
           { %>
        <li>
            <%= Html.ActionLink(product.Title, "Product", new { id = product.ProductID })%>
        </li>
        <% } %>
    </ul>
    <% } %>
    <p>
        <%=Html.ActionLink("Отделы", "Index", null, new { @class = "button" })%>
        <% if (Context.User.IsInRole(Constants.AdministratorsRoleName))
           { %>
        <%= Html.ActionLink("Новый продукт", "CreateProduct", new { Department = Model.Department.DepartmentID }, new { @class = "button new" })%>
        <%= Html.ActionLink("Править", "EditDepartment", new { id = Model.Department.DepartmentID }, new { @class = "button edit" })%>
        <%= Html.ActionLink("Удалить отдел", "DeleteDepartment", new { id = Model.Department.DepartmentID }, new { @class = "button delete" })%>
        <% } %>
    </p>
</asp:Content>
