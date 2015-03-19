<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(String.Format("GrapeFruit Store - Корзина пользователя {0}", User.Identity.Name)) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Корзина</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <% if (Model.OrderItem.ToList().Count == 0)
       { %>
    <p>
        <i>Эта корзина пуста.</i></p>
    <% }
       else
       { %>
    <p>
    </p>
    <table>
        <tr>
            <th>
                №
            </th>
            <th>
                Отдел
            </th>
            <th>
                Наименование
            </th>
            <th>
                Цена
            </th>
            <th>
                Количество
            </th>
            <th>
                Стоимость
            </th>
        </tr>
        <% int i = 1; decimal total = 0; foreach (var item in Model.OrderItem)
           { %>
        <tr>
            <td>
                <%= Html.Encode(i) %>
            </td>
            <td>
                <%= Html.ActionLink(item.Product.Department.Title, "Department", new { id = item.Product.DepartmentID })%>
            </td>
            <td>
                <%= Html.ActionLink(item.Product.Title, "Product", new { id = item.ProductID })%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.UnitPrice)) %>
            </td>
            <td>
                <% using (Html.BeginForm("ChangeShoppingCart", "Store", HttpVerbs.Post))
                   { %>
                <%= Html.Hidden("ProductID", item.ProductID)%>
                <%= Html.TextBox("Quantity", item.Quantity, new { @class = "cell" })%>
                <input type="submit" value="Сохранить" name="change" class="cell" />
                <% } %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F} руб.", item.UnitPrice * item.Quantity)) %>
            </td>
            <td>
                <% using (Html.BeginForm("DeleteShoppingCartItem", "Store", HttpVerbs.Post))
                   { %>
                <%= Html.Hidden("productid", item.ProductID) %>
                <input type="submit" value="Удалить" name="delete" class="cell" />
                <% } %>
            </td>
        </tr>
        <% i++;
           total += item.UnitPrice * item.Quantity;
           } %>
    </table>
    <p>
        <%= Html.Encode(String.Format("Итого: {0:F} руб.", total))%>
    </p>
    <% using (Html.BeginForm("CreateOrder", "Store", HttpVerbs.Post))
       { %>
    <p>
        <input type="submit" value="Оформить заказ" name="createorder" />
    </p>
    <% } %>
    <% } %>
</asp:Content>
