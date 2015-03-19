<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(String.Format("GrapeFruit Store - {0}", Model.Title)) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(Model.Title) %></h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <fieldset>
        <legend>Описание</legend>
        <div class="image">
            <img alt="Картинки нет" src="<%= Html.Encode(Constants.ImagesPath + Model.ImageUrl) %>" />
            <div>
                <b>Цена: </b>
                <%= Html.Encode(String.Format("{0:F} руб.", Model.Price)) %>
                <br />
                <br />
                <b>На складе: </b>
                <%= Html.Encode(String.Format("{0} шт.", Model.UnitsInStock)) %>
            </div>
        </div>
        <p>
            <%= Html.Encode(Model.Description) %>
        </p>
    </fieldset>
    <% if (User.Identity.IsAuthenticated)
       {
           using (Html.BeginForm("AddToShoppingCart", "Store", new { ProductID = Model.ProductID }, FormMethod.Post))
           {
               if (Model.UnitsInStock > 0)
               { %>
    <p>
        <input type="submit" value="В корзину" />
    </p>
    <% }
               else
               { %>
    <p>
        На складе нет товара.
    </p>
    <% }
           }
       }
       else
       { %>
    <p>
        Чтобы положить товар в корзину, необходимо
        <%= Html.ActionLink("войти", "LogOn", "Account", new { ReturnUrl = Request.Url.ToString() }, null) %>
        на сайт.
    </p>
    <% } %>
    <p>
        <%=Html.ActionLink("К отделу", "Department", new { id = Model.DepartmentID }, new { @class = "button" })%>
        <% if (Context.User.IsInRole(Constants.AdministratorsRoleName))
           { %>
        <%= Html.ActionLink("Править", "EditProduct", new { id = Model.ProductID }, new { @class = "button edit" })%>
        <%= Html.ActionLink("Удалить", "DeleteProduct", new { id = Model.ProductID }, new { @class = "button delete" })%>
        <% } %>
    </p>
</asp:Content>
