<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Вход
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Log On</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <p>
        Введите логин и пасс.
        <%= Html.ActionLink("Зарегистрируйтесь", "Register") %>, если нет аккаунта.
    </p>
    <%= Html.ValidationSummary("") %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>Аккаунт</legend>
            <p>
                <label for="username">
                    Логин:</label>
                <%= Html.TextBox("username") %>
                <%= Html.ValidationMessage("username") %>
            </p>
            <p>
                <label for="password">
                    Пароль:</label>
                <%= Html.Password("password") %>
                <%= Html.ValidationMessage("password") %>
            </p>
            <p>
                <%= Html.CheckBox("rememberMe") %>
                <label class="inline" for="rememberMe">
                    Запомнить меня</label>
            </p>
            <p>
                <input type="submit" value="Входт" />
            </p>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
