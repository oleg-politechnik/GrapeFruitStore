<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IQueryable<GrapeFruitStore.Models.Order>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Заказы
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <% if (User.IsInRole(Constants.AdministratorsRoleName))
           {
        %>
        GrapeFruit Store - Администрирование заказов
        <% }
           else
           { %>
        <%= Html.Encode(String.Format("GrapeFruit Store - Заказы пользователя {0}", User.Identity.Name)) %>
        <% } %>
    </h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <% if (Model.ToList().Count == 0)
       { %>
    <p>
        <i>Заказов еще нет.</i></p>
    <% }
       else
       { %>
    <p>
    </p>
    <table>
        <tr>
            <% if (User.IsInRole(Constants.AdministratorsRoleName))
               { %>
            <th>
                Имя пользователя
            </th>
            <% } %>
            <th>
                Дата добавления
            </th>
            <%--<th>
                Статус
            </th>--%>
        </tr>
        <% foreach (var order in Model)
           { %>
        <tr>
            <% if (User.IsInRole(Constants.AdministratorsRoleName))
               { %>
            <td>
                <%= Html.Encode(order.UserName)%>
            </td>
            <% } %>
            <td>
                <%= Html.Encode(String.Format("{0} в {1}", order.AddedDate.ToShortDateString(), order.AddedDate.ToShortTimeString())) %>
            </td>
            <%--<td>
                <%= Html.Encode(order.Status)%>
            </td>--%>
            <td>
                <%= Html.ActionLink("Подробно", "Order", new { id = order.OrderID }) %>
            </td>
        </tr>
        <% } %>
    </table>
    <% } %>
</asp:Content>
